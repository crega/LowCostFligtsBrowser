using LowCostFligtsBrowser.Application.Airports.Queries.SearchFlights;
using LowCostFligtsBrowser.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;

namespace LowCostFligtsBrowser.Infrastructure
{
    public interface IAmadeusClient
    {
        string BaseUrl { get; set; }

        Task<Success2> GetFlightOffersAsync(string originLocationCode, string destinationLocationCode, DateTimeOffset departureDate, DateTimeOffset? returnDate, int adults, int? children, int? infants, TravelClass2? travelClass, string includedAirlineCodes, string excludedAirlineCodes, bool? nonStop, string currencyCode, int? maxPrice, int? max);
        Task<Success2> GetFlightOffersAsync(string originLocationCode, string destinationLocationCode, DateTimeOffset departureDate, DateTimeOffset? returnDate, int adults, int? children, int? infants, TravelClass2? travelClass, string includedAirlineCodes, string excludedAirlineCodes, bool? nonStop, string currencyCode, int? maxPrice, int? max, CancellationToken cancellationToken);
        Task<Success2> GetFlightOffersAsync(SearchFlightsQuery search);
        Task<Success> SearchFlightOffersAsync(string x_HTTP_Method_Override, GetFlightOffersQuery getFlightOffersBody);
        Task<Success> SearchFlightOffersAsync(string x_HTTP_Method_Override, GetFlightOffersQuery getFlightOffersBody, CancellationToken cancellationToken);
        /// <summary>
        /// Duration of In memory caching
        /// In format HH-mm-ss
        /// <para>To define permanent strorage in cache use  "00-00-00" which is <b>NOT ADVISED</b></para>
        /// </summary>
        string DurationOfCache { get; set; }

    }
}