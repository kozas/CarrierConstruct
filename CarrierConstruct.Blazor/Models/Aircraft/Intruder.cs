using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;

namespace CarrierConstruct.Blazor.Models
{
    public class Intruder : IAircraft
    {
        public AircraftStatus Status { get; set; }
        public int Serial { get; }
        public int Modex { get; }

        public string Manufacturer => "Grumman";
        public string Designation => "A-6";
        public string Name => "Intruder";

        public Intruder(int serial, int modex)
        {
            Serial = serial;
            Modex = modex;
        }
    }
}
