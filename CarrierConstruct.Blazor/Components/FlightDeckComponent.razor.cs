using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models;
using CarrierConstruct.Blazor.Models.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarrierConstruct.Blazor.Components
{
    public partial class FlightDeckComponent
    {
        [Parameter]
        public EventCallback<TransferAircraftViaElevatorRequest> OnAircraftOrderedToHangar { get; set; }

        public List<IAircraft> AircraftOnFlightDeck;

        protected override void OnInitialized()
        {
            AircraftOnFlightDeck = new List<IAircraft>();

            AircraftOnFlightDeck.Add(new Intruder(100001, 201));
            AircraftOnFlightDeck.Add(new Intruder(100002, 202));
            AircraftOnFlightDeck.Add(new Intruder(100003, 203));
        }

        //TODO: Make Generic?
        public async Task ReceiveAircraftFromElevator(IAircraft aircraft)
        {
            await Task.Delay(3000);
            AircraftOnFlightDeck.Add(aircraft);
            StateHasChanged();
        }

        //TODO: Make Generic?
        public async Task RemoveAircraftFromFlightDeck(IAircraft aircraft)
        {
            await Task.Delay(1000);
            AircraftOnFlightDeck.Remove(aircraft);
        }

        //TODO: Make Generic?
        //private async Task OrderAircraftToHangar(MouseEventArgs e, IAircraft aircraft)
        //{
        //    var aircraftList = new List<IAircraft>();
        //    aircraftList.Add(aircraft);

        //    var transferRequest = new TransferAircraftViaElevatorRequest(aircraftList, ElevatorLocation.FlightDeck, ElevatorLocation.Hangar);

        //    await OnAircraftOrderedToHangar.InvokeAsync(transferRequest);
        //}


    }
}
