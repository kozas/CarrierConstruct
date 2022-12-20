using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models.Aircraft;
using CarrierConstruct.Blazor.Models.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarrierConstruct.Blazor.Components
{
    public partial class HangarComponent
    {
        [Parameter]
        public EventCallback<TransferAircraftViaElevatorRequest> OnAircraftOrderedToFlightDeck { get; set; }

        public List<IAircraft> AircraftInHangar;

        protected override void OnInitialized()
        {
            AircraftInHangar = new List<IAircraft>();

            AircraftInHangar.Add(new Hornet(100004, 101));
            AircraftInHangar.Add(new Hornet(100005, 102));
            AircraftInHangar.Add(new Hornet(100006, 103));
            AircraftInHangar.Add(new Hornet(100007, 201));
            AircraftInHangar.Add(new Hornet(100008, 202));
            AircraftInHangar.Add(new Hornet(100009, 203));
        }

        //TODO: Make Generic?
        public async Task ReceiveAircraftFromElevator(IAircraft aircraft)
        {
            await Task.Delay(3000);
            AircraftInHangar.Add(aircraft);
            StateHasChanged();
        }

        //TODO: Make Generic?
        public async Task RemoveAircraftFromHangar(IAircraft aircraft)
        {
            await Task.Delay(1000);
            AircraftInHangar.Remove(aircraft);
        }

        //TODO: Make Generic?
        private async Task OrderAircraftToFlightDeck(MouseEventArgs e, IAircraft aircraft)
        {

            var aircraftList = new List<IAircraft>();
            aircraftList.Add(aircraft);

            var transferRequest = new TransferAircraftViaElevatorRequest(aircraftList, ElevatorLocation.Hangar, ElevatorLocation.FlightDeck);

            await OnAircraftOrderedToFlightDeck.InvokeAsync(transferRequest);
        }

        
    }
}