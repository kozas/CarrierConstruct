using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models;
using CarrierConstruct.Blazor.Models.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarrierConstruct.Blazor.Components;

public partial class FlightDeckComponent
{
    public List<IAircraft> AircraftOnFlightDeck;
    private bool orderInProgress = false;
    private List<IAircraft> selectedAircraft;

    [Parameter]
    public EventCallback<TransferAircraftViaElevatorRequest> OnAircraftOrderedToHangar { get; set; }

    //TODO: Make Generic?
    public async Task ReceiveAircraftFromElevator(IAircraft aircraft)
    {
        AircraftOnFlightDeck.Add(aircraft);
        StateHasChanged();
    }

    //TODO: Make Generic?
    public async Task RemoveAircraftFromFlightDeck(IAircraft aircraft)
    {
        AircraftOnFlightDeck.Remove(aircraft);
        StateHasChanged();
    }

    public void SetOrderInProgress(bool isInProgress)
    {
        orderInProgress = isInProgress;
    }

    public void ClearSelectedAircraft()
    {
        selectedAircraft.Clear();
    }

    protected override async Task OnInitializedAsync()
    {
        AircraftOnFlightDeck = new List<IAircraft>();
        selectedAircraft = new List<IAircraft>();

        await LoadAircraftList();
    }

    private async Task OrderSelectedAircraftToHangar()
    {
        if (selectedAircraft.Count == 0)
        {
            return;
        }

        var transferRequest = new TransferAircraftViaElevatorRequest(selectedAircraft, ElevatorLocation.FlightDeck, ElevatorLocation.Hangar);
        await OnAircraftOrderedToHangar.InvokeAsync(transferRequest);
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
        AircraftOnFlightDeck.Add(new Intruder(100001, 201));
        AircraftOnFlightDeck.Add(new Intruder(100002, 202));
        AircraftOnFlightDeck.Add(new Intruder(100003, 203));
    }
}