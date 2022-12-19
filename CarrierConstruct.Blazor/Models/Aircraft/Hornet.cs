using CarrierConstruct.Blazor.Interfaces;

namespace CarrierConstruct.Blazor.Models.Aircraft
{
    public class Hornet : IAircraft
    {
        public int Serial { get; }
        public int Modex { get; }

        public string Manufacturer => "McDonnell Douglas";
        public string Designation => "F/A-18";
        public string Name => "Hornet";

        public Hornet(int serial, int modex)
        {
            Serial = serial;
            Modex = modex;
        }
    }
}
