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
        private List<IAircraft> selectedAircraft;

        protected override async Task OnInitializedAsync()
        {
            AircraftInHangar = new List<IAircraft>();
            selectedAircraft = new List<IAircraft>();

            await LoadAircraftList();
        }

        //TODO: Make Generic?
        public async Task ReceiveAircraftFromElevator(IAircraft aircraft)
        {
            //await Task.Delay(3000);
            AircraftInHangar.Add(aircraft);
            StateHasChanged();
        }

        //TODO: Make Generic?
        public async Task RemoveAircraftFromHangar(IAircraft aircraft)
        {
            //await Task.Delay(1000);
            AircraftInHangar.Remove(aircraft);
            StateHasChanged();
        }

        //TODO: Make Generic?
        private async Task OrderSelectedAircraftToFlightDeck()
        {
            var transferRequest = new TransferAircraftViaElevatorRequest(selectedAircraft, ElevatorLocation.Hangar, ElevatorLocation.FlightDeck);

            await OnAircraftOrderedToFlightDeck.InvokeAsync(transferRequest);
        }

        private void SelectAircraft(MouseEventArgs e, IAircraft aircraft)
        {
            if (selectedAircraft.Contains(aircraft))
            {
                selectedAircraft.Remove(aircraft);
            }
            else
            {
                selectedAircraft.Add(aircraft);
            }
        }

        private async Task LoadAircraftList()
        {
            AircraftInHangar.Add(new Hornet(100004, 101));
            AircraftInHangar.Add(new Hornet(100005, 102));
            AircraftInHangar.Add(new Hornet(100006, 103));
            AircraftInHangar.Add(new Hornet(100007, 201));
            AircraftInHangar.Add(new Hornet(100008, 202));
            AircraftInHangar.Add(new Hornet(100009, 203));
        }

        
    }
}