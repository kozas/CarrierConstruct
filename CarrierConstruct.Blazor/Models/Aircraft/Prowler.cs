using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;

namespace CarrierConstruct.Blazor.Models
{
    public class Prowler : IAircraft
    {
        public AircraftStatus Status { get; set; }
        public int Serial { get; }
        public int Modex { get; }

        public string Manufacturer => "Grumman";
        public string Designation => "EA-6B";
        public string Name => "Prowler";
        public AircraftRole Role => AircraftRole.EWAR;

        public Prowler(int serial, int modex)
        {
            Serial = serial;
            Modex = modex;
        }
    }
}
