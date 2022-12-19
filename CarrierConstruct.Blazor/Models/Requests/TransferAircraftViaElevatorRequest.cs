using CarrierConstruct.Blazor.Enums;
using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models.ShipSystems;

namespace CarrierConstruct.Blazor.Models.Requests
{
    public class TransferAircraftViaElevatorRequest
    {
        public TransferAircraftViaElevatorRequest(List<IAircraft> aircraftList, ElevatorLocation origin, ElevatorLocation destination)
        {
            AircraftList = aircraftList;
            Origin = origin;
            Destination = destination;
        }

        public List<IAircraft> AircraftList { get; set; }
        public ElevatorLocation Origin { get; set; }
        public ElevatorLocation Destination { get; set; }
    }
}
