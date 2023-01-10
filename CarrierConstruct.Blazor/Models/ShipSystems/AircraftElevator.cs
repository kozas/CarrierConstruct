using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;

namespace CarrierConstruct.Blazor.Models.ShipSystems;

public class AircraftElevator
{
    public string StatusMessage = "";

    public AircraftElevator(int number, int capacity, int speed)
    {
        ElevatorNumber = number;
        Capacity = capacity;
        Speed = speed;
        OrderedAircraftSerials = new List<int>();
        AircraftOnElevator = new List<IAircraft>();
    }

    public int ElevatorNumber { get; set; }
    public int Capacity { get; set; }
    public int Speed { get; set; }
    public List<int>? OrderedAircraftSerials { get; set; }
    public List<IAircraft>? AircraftOnElevator { get; set; }
    public ElevatorLocation Location { get; set; } = ElevatorLocation.FlightDeck;
    public bool isLoading { get; private set; }

    public async Task LoadAircraftOnElevator(IAircraft aircraft)
    {
        isLoading = true;
        SetStatusMessage($"Loading {aircraft.Name}...");
        await Task.Delay(800);
        AircraftOnElevator?.Add(aircraft);
        isLoading = false;
        SetStatusMessage($"{aircraft.Name} loaded!");
    }

    public async Task UnloadAircraftFromElevator(IAircraft aircraft)
    {
        //isLoading = true;
        //SetStatusMessage($"Unloading {aircraft.Name}...");

        await Task.Delay(1000);
        AircraftOnElevator?.Remove(aircraft);
        //isLoading = false;
        //SetStatusMessage($"{aircraft.Name} unloaded!");
    }

    public async Task TravelToLocation(ElevatorLocation location)
    {
        if (location == Location)
        {
            return;
        }

        Location = ElevatorLocation.InTransit;

        await Task.Delay(Speed * 1000);
        Location = location;
    }

    private void SetStatusMessage(string message)
    {
        StatusMessage = message;
    }
}