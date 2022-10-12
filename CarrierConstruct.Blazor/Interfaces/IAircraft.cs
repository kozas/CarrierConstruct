namespace CarrierConstruct.Blazor.Interfaces
{
    public interface IAircraft
    {
        public int Serial { get; }
        public int Modex { get; }
        public string Manufacturer { get; }
        public string Designation { get; }
        public string Name { get; }
    }
}
