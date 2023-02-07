using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;

namespace CarrierConstruct.Blazor.Models
{
    public class Hawkeye : IAircraft
    {
        public AircraftStatus Status { get; set; }
        public int Serial { get; }
        public int Modex { get; }

        public string Manufacturer => "Northrop Grumman";
        public string Designation => "E-2";
        public string Name => "Hawkeye";
        public AircraftRole Role => AircraftRole.AWACS;

        public Hawkeye(int serial, int modex)
        {
            Serial = serial;
            Modex = modex;
        }
    }
}
