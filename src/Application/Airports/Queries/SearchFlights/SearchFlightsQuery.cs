using AutoMapper;
using AutoMapper.QueryableExtensions;
using LowCostFligtsBrowser.Application.Common.Interfaces;
using LowCostFligtsBrowser.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Airports.Queries.SearchFlights
{
    public class SearchFlightsQuery : IRequest<SearchFlightsVM>, IEquatable<SearchFlightsQuery>
    {
        #region Public Properites
        public string OriginIATACode { get; set; }
        public string DestinationIATACode { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public int NumberOfAdults { get; set; }
        public DateTimeOffset? ReturnDate { get; set; }
        public int? NumberOfChildren { get; set; }
        public int? NumberOfInfants { get; set; }
        public int? TravelClass { get; set; }
        public string? IncludedAirlineCodes { get; set; }
        public string? ExcludedAirlineCodes { get; set; }
        public bool? NoStops { get; set; }
        public string? CurrencyCode { get; set; }
        public int? MaxPrice { get; set; }
        public int? Max { get; set; }
        #endregion

        #region GetHash and Equals 
        public override bool Equals(object obj)
        {
            return Equals(obj as SearchFlightsQuery);
        }

        public bool Equals(SearchFlightsQuery other)
        {
            return other != null &&
                   OriginIATACode == other.OriginIATACode &&
                   DestinationIATACode == other.DestinationIATACode &&
                   DepartureTime.Equals(other.DepartureTime) &&
                   NumberOfAdults == other.NumberOfAdults &&
                   EqualityComparer<DateTimeOffset?>.Default.Equals(ReturnDate, other.ReturnDate) &&
                   NumberOfChildren == other.NumberOfChildren &&
                   NumberOfInfants == other.NumberOfInfants &&
                   TravelClass == other.TravelClass &&
                   IncludedAirlineCodes == other.IncludedAirlineCodes &&
                   ExcludedAirlineCodes == other.ExcludedAirlineCodes &&
                   NoStops == other.NoStops &&
                   CurrencyCode == other.CurrencyCode &&
                   MaxPrice == other.MaxPrice &&
                   Max == other.Max;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(OriginIATACode);
            hash.Add(DestinationIATACode);
            hash.Add(DepartureTime);
            hash.Add(NumberOfAdults);
            hash.Add(ReturnDate);
            hash.Add(NumberOfChildren);
            hash.Add(NumberOfInfants);
            hash.Add(TravelClass);
            hash.Add(IncludedAirlineCodes);
            hash.Add(ExcludedAirlineCodes);
            hash.Add(NoStops);
            hash.Add(CurrencyCode);
            hash.Add(MaxPrice);
            hash.Add(Max);
            return hash.ToHashCode();
        }
        #endregion

    }
    public class SearchFlightsQueryHandler
        : IRequestHandler<SearchFlightsQuery, SearchFlightsVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAmadeusClient _client;
        private readonly IMemoryCache  _inMemoryCache;
        public SearchFlightsQueryHandler(IMapper mapper, IApplicationDbContext context, IAmadeusClient apiServiceClient, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _client = apiServiceClient;
            _inMemoryCache = memoryCache;
        }

        public Task<SearchFlightsVM> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
        {
            SearchFlightsVM cachedRequestResult;
            int cacheId = request.GetHashCode();
            cachedRequestResult = _inMemoryCache.Get<SearchFlightsVM>(cacheId);
            if (cachedRequestResult != null)
            {
                return Task.FromResult(cachedRequestResult);
            }
            Task<Success2> searchData = _client.GetFlightOffersAsync(request);
            var serviceResult = searchData.Result.Data;
            var resultq = serviceResult.AsQueryable();
            var resulta = resultq.ProjectTo<SearchFlightsDTO>(_mapper.ConfigurationProvider);
            var result = resulta.OrderBy(t => t.Id).ToList();
            var toReturn = new SearchFlightsVM()
            {
                SearchedResult = result
            };
            DateTimeOffset durationOfCache = CalculateCacheTimeout();
            _inMemoryCache.Set<SearchFlightsVM>(cacheId, toReturn, durationOfCache);
            return Task.FromResult(toReturn);

        }
        /// <summary>
        /// Calculates duration of validity of an in memory cache entry.
        /// </summary>
        /// <returns></returns>
        private DateTimeOffset CalculateCacheTimeout()
        {
            var durationOfCacheArrayToParse = _client.DurationOfCache.Split('-');
            TimeSpan ts = new TimeSpan(int.Parse(durationOfCacheArrayToParse[0]), int.Parse(durationOfCacheArrayToParse[1]), int.Parse(durationOfCacheArrayToParse[2]));
            DateTimeOffset durationOfCache = DateTimeOffset.Now.Add(ts);
            return durationOfCache;
        }
    }
}

