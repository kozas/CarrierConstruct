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
        protected AircraftStatus Status { get; set; }

        public void SetStatus(AircraftStatus status)
        {
            Status = status;
        }
    }
}
