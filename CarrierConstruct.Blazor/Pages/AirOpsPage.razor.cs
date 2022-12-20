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
                new AircraftElevator(1, 1, 4),
                new AircraftElevator(2, 1, 4),
                new AircraftElevator(3, 1, 4)
            };
        }

        private async void TransferAircraftViaElevator(TransferAircraftViaElevatorRequest request)
        {
            var selectedElevator = DetermineBestElevator(request);
            if (selectedElevator is null)
            {
                return;
                //TODO: Error Handling
            }

            await selectedElevator.TravelToLocation(request.Origin);
            StateHasChanged();

            foreach (var aircraft in request.AircraftList)
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

                await selectedElevator.LoadAircraftOnElevator(aircraft);
                StateHasChanged();
            }

            await selectedElevator.TravelToLocation(request.Destination);
            StateHasChanged();

            foreach (var aircraft in request.AircraftList)
            {
                switch (selectedElevator.Location)
                {
                    case ElevatorLocation.Hangar:
                        await hangarComponent.ReceiveAircraftFromElevator(aircraft);
                        break;
                    case ElevatorLocation.FlightDeck:
                        await flightDeckComponent.ReceiveAircraftFromElevator(aircraft);
                        break;
                }

                await selectedElevator.UnloadAircraftFromElevator(aircraft);
                StateHasChanged();
            }
        }

        private AircraftElevator? DetermineBestElevator(TransferAircraftViaElevatorRequest request)
        {
            //TODO: Improve logic
            //TODO: Elevator capacity logic

            foreach (var elevator in aircraftElevators)
            {
                if (elevator.Location != ElevatorLocation.InTransit && elevator.AircraftOnElevator?.Count == 0)
                {
                    return elevator;
                }
            }

            return null;
        }
    }

}
