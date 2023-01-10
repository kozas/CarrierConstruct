using CarrierConstruct.Blazor.Enums;

namespace CarrierConstruct.Blazor.Interfaces
{
    public interface IAircraft
    {
        public int Serial { get; }
        public int Modex { get; }
        public string Manufacturer { get; }
        public string Designation { get; }
        public string Name { get; }
        public string FullName => Manufacturer + Designation + Name;
        public AircraftRole Role { get; }
        public AircraftStatus Status { get; protected set; }

        public void SetStatus(AircraftStatus status)
        {
            Status = status;
        }
    }
}
