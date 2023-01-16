using CarrierConstruct.Blazor.Components;
using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models.Requests;
using CarrierConstruct.Blazor.Models.ShipSystems;
using Microsoft.AspNetCore.Components;

namespace CarrierConstruct.Blazor.Pages;

public partial class AirOpsPage
{
    private List<AircraftElevator>? aircraftElevators;

    private FlightDeckComponent? flightDeckComponent;
    private HangarComponent? hangarComponent;

    [Parameter]
    public EventCallback<List<IAircraft>> OnAircraftArrivedAtFlightDeck { get; set; }

    [Parameter]
    public EventCallback<List<IAircraft>> OnAircraftArrivedAtHangar { get; set; }

    [Inject]
    private AppState? AppState { get; set; }

    protected override void OnInitialized()
    {
        aircraftElevators = new List<AircraftElevator>
        {
            new AircraftElevator(1, 2, 4),
            new AircraftElevator(2, 2, 4),
            new AircraftElevator(3, 2, 6)
        };
    }

    private async void ProcessAircraftTransferRequest(TransferAircraftViaElevatorRequest request)
    {
        hangarComponent.SetOrderInProgress(true);

        var assignedAircraftSerials = 0;
        var assignedElevators = new List<AircraftElevator>();
        var tasks = new List<Task>();

        //// Assign elevators and their aircraft
        while (assignedAircraftSerials < request.AircraftList.Count)
        {
            foreach (var elevator in aircraftElevators)
            {
                for (var i = 0; i < elevator.Capacity; i++)
                {
                    if (assignedAircraftSerials == request.AircraftList.Count)
                    {
                        continue;
                    }

                    elevator.OrderedAircraftSerials.Add(request.AircraftList[assignedAircraftSerials].Serial);
                    assignedAircraftSerials++;
                    StateHasChanged();
                }

                if (elevator.OrderedAircraftSerials.Count > 0 && !assignedElevators.Contains(elevator))
                {
                    assignedElevators.Add(elevator);
                }
            }
        }

        // Execute orders for assigned elevators
        foreach (var elevator in assignedElevators)
        {
            var response = TransferAircraftViaElevator(request, elevator);
            tasks.Add(response);
        }

        // Wait for all elevators to complete their orders
        await Task.WhenAll(tasks);
        hangarComponent.ClearSelectedAircraft();
        hangarComponent.SetOrderInProgress(false);
        await InvokeAsync(() => StateHasChanged());
    }

    // TODO: figure out why aircraft are removed from hanger too early on UI
    private async Task TransferAircraftViaElevator(TransferAircraftViaElevatorRequest request, AircraftElevator elevator)
    {
        while (elevator.OrderedAircraftSerials.Count > 0)
        {
            await elevator.TravelToLocation(request.Origin);
            StateHasChanged();

            foreach (var aircraft in request.AircraftList)
            {
                if (elevator.OrderedAircraftSerials.Contains(aircraft.Serial))
                {
                    aircraft.SetStatus(AircraftStatus.InTransit);

                    if (elevator.AircraftOnElevator.Count < elevator.Capacity)
                    {
                        switch (request.Origin)
                        {
                            case ElevatorLocation.Hangar:
                                await hangarComponent.RemoveAircraftFromHangar(aircraft);
                                break;
                            case ElevatorLocation.FlightDeck:
                                await flightDeckComponent.RemoveAircraftFromFlightDeck(aircraft);
                                break;
                        }

                        elevator.OrderedAircraftSerials.Remove(aircraft.Serial);
                        await elevator.LoadAircraftOnElevator(aircraft);
                    }

                    StateHasChanged();
                }
            }

            await elevator.TravelToLocation(request.Destination);

            StateHasChanged();

            //todo: make this not stupid
            while (elevator.AircraftOnElevator.Count > 0)
            {
                var aircraft = elevator.AircraftOnElevator[0];

                switch (elevator.Location)
                {
                    case ElevatorLocation.Hangar:
                        await hangarComponent.ReceiveAircraftFromElevator(aircraft);
                        break;
                    case ElevatorLocation.FlightDeck:
                        await flightDeckComponent.ReceiveAircraftFromElevator(aircraft);
                        break;
                }

                await elevator.UnloadAircraftFromElevator(aircraft);
                aircraft.SetStatus(AircraftStatus.Idle);
            }
        }
    }

    //private AircraftElevator? DetermineBestElevator(TransferAircraftViaElevatorRequest request)
    //{
    //    //TODO: Improve logic
    //    //TODO: Elevator capacity logic

    //    foreach (var elevator in aircraftElevators)
    //    {
    //        if (elevator.Location != ElevatorLocation.InTransit && elevator.AircraftOnElevator?.Count == 0)
    //        {
    //            return elevator;
    //        }
    //    }

    //    return null;
    //}
}