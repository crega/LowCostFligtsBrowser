using AutoMapper;
using LowCostFligtsBrowser.Application.Airports.Queries.GetAirports;
using LowCostFligtsBrowser.Application.Airports.Queries.SearchFlights;
using LowCostFligtsBrowser.Application.Common.Interfaces;
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

                profile.CreateMap<FlightOffer, SearchFlightsDTO>()
                    .ForMember(dest => dest.Id, m => m.MapFrom(src => int.Parse(src.Id)))
                    .ForMember(dest => dest.Price, m => m.MapFrom(src => src.Price.GrandTotal))
                    .ForMember(dest => dest.Currency, m => m.MapFrom(src => src.Price.Currency))
                    .ForMember(dest => dest.DepartureAirport, m => m.MapFrom(src => src.Itineraries.First().Segments.First().Departure.IataCode))
                    .ForMember(dest => dest.DateOfDeparture, m => m.MapFrom(src => src.Itineraries.First().Segments.First().Departure.At.Value.DateTime))
                    .ForMember(dest => dest.ArrivalAirport, m => m.MapFrom(src => src.Itineraries.Last().Segments.Last().Arrival.IataCode))
                    .ForMember(dest => dest.DateOfArrival, m => m.MapFrom(src => src.Itineraries.Last().Segments.Last().Arrival.At.Value.DateTime))
                    .ForMember(dest => dest.NumberOfItinerariesArrival, m => m.MapFrom(src => src.Itineraries.SelectMany(i => i.Segments.Select(s => s.Arrival.IataCode)).Count()))
                    .ForMember(dest => dest.NumberOfItinerariesDeparture, m => m.MapFrom(src => src.Itineraries.SelectMany(i => i.Segments.Select(s => s.Departure.IataCode)).Count()))
                    .ForMember(dest => dest.NumberOfAdults, m => m.MapFrom(src => src.TravelerPricings.Count(ad => ad.TravelerType == TravelerType.ADULT)))
                    .ForMember(dest => dest.NumberOfChildren, m => m.MapFrom(src => src.TravelerPricings.Count(ad => ad.TravelerType == TravelerType.CHILD)))
                    .ForMember(dest => dest.NumberOfInfants, m => m.MapFrom(src => src.TravelerPricings.Count(ad => ad.TravelerType == TravelerType.HELD_INFANT
                                                                                                                 || ad.TravelerType == TravelerType.HELD_INFANT)));





            }
        }
    }
}
