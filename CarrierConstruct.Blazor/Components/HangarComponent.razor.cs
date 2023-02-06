using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models;
using CarrierConstruct.Blazor.Models.Aircraft;
using CarrierConstruct.Blazor.Models.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarrierConstruct.Blazor.Components;

public partial class HangarComponent
{
    public List<IAircraft> AircraftInHangar;
    private bool orderInProgress = false;
    private List<IAircraft> selectedAircraft;

    [Parameter]
    public EventCallback<TransferAircraftViaElevatorRequest> OnAircraftOrderedToFlightDeck { get; set; }

    //TODO: Make Generic?
    public async Task ReceiveAircraftFromElevator(IAircraft aircraft)
    {
        AircraftInHangar.Add(aircraft);
        StateHasChanged();
    }

    //TODO: Make Generic?
    public async Task RemoveAircraftFromHangar(IAircraft aircraft)
    {
        AircraftInHangar.Remove(aircraft);
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
        AircraftInHangar = new List<IAircraft>();
        selectedAircraft = new List<IAircraft>();

        await LoadAircraftList();
    }

    private async Task OrderSelectedAircraftToFlightDeck()
    {
        if (selectedAircraft.Count == 0)
        {
            return;
        }

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
        //AircraftInHangar.Add(new Hornet(100007, 201));
        //AircraftInHangar.Add(new Hornet(100008, 202));
        //AircraftInHangar.Add(new Hornet(100009, 203));
        //AircraftInHangar.Add(new Intruder(100010, 301));
        //AircraftInHangar.Add(new Intruder(100011, 302));
        //AircraftInHangar.Add(new Hornet(100013, 401));
        //AircraftInHangar.Add(new Hornet(100014, 402));
        //AircraftInHangar.Add(new Hornet(100015, 403));
        //AircraftInHangar.Add(new Hornet(100016, 404));
        //AircraftInHangar.Add(new Hornet(100017, 405));
    }
}