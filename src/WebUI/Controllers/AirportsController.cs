using Afonsoft.Amadeus;
using LowCostFligtsBrowser.Application.Airports.Queries.GetAirports;
using LowCostFligtsBrowser.Application.Airports.Queries.SearchFlights;
using LowCostFligtsBrowser.Application.Common.Models;
using LowCostFligtsBrowser.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using LowCostFligtsBrowser.Domain.Entities;
using LowCostFligtsBrowser.Infrastructure;
using LowCostFligtsBrowser.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.WebUI.Controllers
{
    public class AirportController : ApiControllerBase
    {
        private IAmadeusClient _client;
        private IConfiguration _configuration;
        public AirportController(IAmadeusClient client,IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<AirportVM> Get()
        {
            return await Mediator.Send(new GetAllAirpotsQuery());

        }
        [HttpGet("GetAllFlights")]
        public IActionResult GetAllFlights()
        {

            
            //client.SearchFlightOffers(null, new GetFlightOffersQuery()
            //{ });
            var searchData = _client.GetFlightOffersAsync("ZAG", "DBV", DateTimeOffset.Now.Date, null, 1, null, null, null, "OU", null, true, "EUR", null, null);
            var result = searchData.Result.Data;
            return Ok(result);


        }
        [HttpPost("SearchFlights")]
        public async Task<PaginatedList<SearchFlightsDTO>> SearchFlights([FromBody] SearchFlightsQuery searchQuery
            )
        {
            return await Mediator.Send(searchQuery);
        }
    }
}

