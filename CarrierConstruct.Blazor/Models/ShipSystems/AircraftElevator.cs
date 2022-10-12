using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;

namespace CarrierConstruct.Blazor.Models.ShipSystems
{
    public class AircraftElevator
    {
        public int ElevatorNumber { get; set; }
        public int Capacity { get; set; }
        public int Speed { get; set; }
        public List<IAircraft>? AircraftOnElevator { get; set; }
        public ElevatorLocation Location { get; set; }

        public AircraftElevator(int number, int capacity, int speed)
        {
            ElevatorNumber = number;
            Capacity = capacity;
            Speed = speed;
        }
    }
}
