using CarrierConstruct.Blazor.Components;
using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models.Requests;
using CarrierConstruct.Blazor.Models.ShipSystems;
using Microsoft.AspNetCore.Components;

namespace CarrierConstruct.Blazor.Pages
{
    public partial class AirOpsPage
    {
        [Inject] private AppState AppState { get; set; }

        [Parameter]
        public EventCallback<List<IAircraft>> OnAircraftArrivedAtFlightDeck { get; set; }

        [Parameter]
        public EventCallback<List<IAircraft>> OnAircraftArrivedAtHangar { get; set; }

        private FlightDeckComponent flightDeckComponent;
        private HangarComponent hangarComponent;

        private List<AircraftElevator>? aircraftElevators;

        protected override void OnInitialized()
        {
            aircraftElevators = new List<AircraftElevator>
            {
                new AircraftElevator(1, 2, 4),
                new AircraftElevator(2, 2, 4),
                new AircraftElevator(3, 2, 4)
            };
        }

        private async void TransferAircraftViaElevator(TransferAircraftViaElevatorRequest request)
        {
            hangarComponent.SetOrderInProgress(true);

            var assignedElevatorSpaces = 0;
            var assignedElevators = new List<AircraftElevator>();

            // Assign elevators and their aircraft
            foreach (var elevator in aircraftElevators)
            {
                while (elevator.OrderedAircraftSerials.Count < elevator.Capacity && assignedElevatorSpaces < request.AircraftList.Count)
                {
                    elevator.OrderedAircraftSerials.Add(request.AircraftList[assignedElevatorSpaces].Serial);
                    assignedElevatorSpaces++;
                    StateHasChanged();
                }

                if (elevator.OrderedAircraftSerials.Count > 0)
                {
                    assignedElevators.Add(elevator);
                }
            }

            // Execute orders for assigned elevators
            foreach (var elevator in assignedElevators)
            {
                // TODO: Await or no await?
                await elevator.TravelToLocation(request.Origin);
                StateHasChanged();

                foreach (var aircraft in request.AircraftList)
                {
                    if (!elevator.OrderedAircraftSerials.Contains(aircraft.Serial))
                    {
                        continue;
                    }

                    aircraft.SetStatus(AircraftStatus.InTransit);

                    switch (request.Origin)
                    {
                        case ElevatorLocation.Hangar:
                            await hangarComponent.RemoveAircraftFromHangar(aircraft);
                            break;
                        case ElevatorLocation.FlightDeck:
                            await flightDeckComponent.RemoveAircraftFromFlightDeck(aircraft);
                            break;
                    }

                    await elevator.LoadAircraftOnElevator(aircraft);
                    StateHasChanged();
                }

                await elevator.TravelToLocation(request.Destination);
                StateHasChanged();

                //todo: make this not stupid
                while(elevator.AircraftOnElevator.Count > 0)
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
            
            hangarComponent.ClearSelectedAircraft();
            hangarComponent.SetOrderInProgress(false);
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

}
