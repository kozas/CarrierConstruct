using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models;
using CarrierConstruct.Blazor.Models.Aircraft;
using CarrierConstruct.Blazor.Models.Requests;
using CarrierConstruct.Blazor.Models.ShipSystems;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace CarrierConstruct.Blazor.Components
{
    public partial class HangarComponent
    {
        [Parameter]
        public EventCallback<TransferAircraftViaElevatorRequest> OnAircraftOrderedToFlightDeck { get; set; }

        public List<IAircraft> AircraftInHangar;

        private List<IAircraft> selectedAircraft;

        protected override void OnInitialized()
        {
            // PLACEHOLDER
            AircraftInHangar = new List<IAircraft>();

            AircraftInHangar.Add(new Hornet(100004, 101));
            AircraftInHangar.Add(new Hornet(100005, 102));
            AircraftInHangar.Add(new Hornet(100006, 103));
        }

        private async Task OrderAircraftToFlightDeck(MouseEventArgs e, IAircraft aircraft)
        {
            AircraftInHangar.Remove(aircraft);

            var aircraftList = new List<IAircraft>();
            aircraftList.Add(aircraft);

            var transferRequest = new TransferAircraftViaElevatorRequest(aircraftList, ElevatorLocation.Hangar, ElevatorLocation.FlightDeck);

            await OnAircraftOrderedToFlightDeck.InvokeAsync(transferRequest);
        }

        //private void SelectAircraft(ChangeEventArgs e, IAircraft aircraft)
        //{

        //}
    }
}