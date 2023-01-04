using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;

namespace CarrierConstruct.Blazor.Models.ShipSystems
{
    public class AircraftElevator
    {
        public int ElevatorNumber { get; set; }
        public int Capacity { get; set; }
        public int Speed { get; set; }
        public List<int>? OrderedAircraftSerials { get; set; }
        public List<IAircraft>? AircraftOnElevator { get; set; }
        public ElevatorLocation Location { get; set; } = ElevatorLocation.FlightDeck;

        public AircraftElevator(int number, int capacity, int speed)
        {
            ElevatorNumber = number;
            Capacity = capacity;
            Speed = speed;
            OrderedAircraftSerials = new List<int>();
            AircraftOnElevator = new List<IAircraft>();
        }

        public async Task LoadAircraftOnElevator(IAircraft aircraft)
        {
            await Task.Delay(500);
            AircraftOnElevator?.Add(aircraft);
        }

        public async Task UnloadAircraftFromElevator(IAircraft aircraft)
        {
            await Task.Delay(500);
            AircraftOnElevator?.Remove(aircraft);
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
    }
}
