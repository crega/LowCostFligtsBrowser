using AutoMapper;
using LowCostFligtsBrowser.Application.Airports.Queries.GetAirports;
using LowCostFligtsBrowser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Common.Mappings
{
    public class ExplicitMappings
    {
        public class BasicAirportData :IMapFrom<Airport>
        {
            public void Mapping(Profile profile) 
            {
                profile.CreateMap<Airport, AirportDTO>()
                    .ForMember(dest => dest.IATACode, m => m.MapFrom(src => src.IATACode))
                    .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
                    .ForMember(dest => dest.AirportName, m => m.MapFrom(src => src.AirportName));
            }
        }
    }
}
