using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Domain.Entities
{
    public class Airport
    {
        public int Id { get; set; }
        public string GMT { get; set; }
        public string IATACode { get; set; }
        public string CityIATACode { get; set; }
        public string ICAOCode { get; set; }
        public string CountryISO2 { get; set; }
        public long? GeonameId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string AirportName { get; set; }
        public string CountryName { get; set; }
        public string PhoneNumber { get; set; }
        public string TimeZone { get; set; }

    }
}
