using CarrierConstruct.Blazor.Components;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models.ShipSystems;
using Microsoft.AspNetCore.Components;

namespace CarrierConstruct.Blazor.Pages
{
    public partial class AirOpsPage
    {
        private FlightDeckComponent flightDeckComponent;
        private HangarComponent hangarComponent;

        private List<AircraftElevator>? aircraftElevators;

        protected override void OnInitialized()
        {
            aircraftElevators = new List<AircraftElevator>
            {
                new AircraftElevator(1, 1, 5),
                new AircraftElevator(2, 1, 5),
                new AircraftElevator(3, 1, 5)
            };
        }

        private void MoveAircraftToFlightDeck(IAircraft aircraft)
        {
            aircraftElevators[0].AircraftOnElevator.Add(aircraft);
            StateHasChanged();
        }
    }

}
