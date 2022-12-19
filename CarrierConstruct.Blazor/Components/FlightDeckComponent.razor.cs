using CarrierConstruct.Blazor.Interfaces;
using CarrierConstruct.Blazor.Models;
using CarrierConstruct.Blazor.Models.ShipSystems;

namespace CarrierConstruct.Blazor.Components
{
    public partial class FlightDeckComponent
    {
        //public List<IAircraft> AircraftInHangar;
        public List<IAircraft> AircraftOnFlightDeck;
        //public List<AircraftElevator> AircraftElevators;

        protected override void OnInitialized()
        {
            // PLACEHOLDER
            //AircraftInHangar = new List<IAircraft>();
            AircraftOnFlightDeck = new List<IAircraft>();
            //AircraftElevators = new List<AircraftElevator>();

            AircraftOnFlightDeck.Add(new Intruder(100001, 201));
            AircraftOnFlightDeck.Add(new Intruder(100002, 202));
            AircraftOnFlightDeck.Add(new Intruder(100003, 203));

            //AircraftElevators.Add(new AircraftElevator(1, 1, 5));
            //AircraftElevators.Add(new AircraftElevator(2, 1, 5));
            //AircraftElevators.Add(new AircraftElevator(3, 1, 5));
            //
        }
    }
}
