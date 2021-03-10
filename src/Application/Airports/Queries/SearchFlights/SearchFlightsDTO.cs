using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Airports.Queries.SearchFlights
{
    public class SearchFlightsDTO
    {
        public int Id { get; set; }
        public string DepartureAirport{ get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }

        public int NumberOfItinerariesArrival { get; set; }
        public int NumberOfItinerariesDeparture { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfInfants { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }
    }
}




