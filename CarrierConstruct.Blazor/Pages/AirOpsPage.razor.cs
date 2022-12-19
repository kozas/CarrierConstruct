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
        public EventCallback<IAircraft> OnAircraftSentToFlightDeck { get; set; }

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

        //private void MoveAircraftToFlightDeck(IAircraft aircraft)
        //{
        //    aircraftElevators[0].AircraftOnElevator.Add(aircraft);

        //    StateHasChanged();
        //}

        private async void TransferAircraftViaElevator(TransferAircraftViaElevatorRequest request)
        {
            //TODO: Determine best elevator 
            var selectedElevator = aircraftElevators[0];

            await selectedElevator.TravelToLocation(request.Origin);

            foreach (var aircraft in request.AircraftList)
            {
                await selectedElevator.LoadAircraftOnElevator(aircraft);
                StateHasChanged();
            }

            await selectedElevator.TravelToLocation(request.Destination);
            StateHasChanged();
        }
    }

}
