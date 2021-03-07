using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Airports.Queries.GetAirports
{
   public class AirportDTO
    {
        public int Id { get; set; }
        public string AirportName { get; set; }
        public string IATACode { get; set; }
    }
}
