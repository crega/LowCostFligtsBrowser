using LowCostFligtsBrowser.Application.Airports.Queries.GetAirports;
using LowCostFligtsBrowser.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using LowCostFligtsBrowser.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.WebUI.Controllers
{
    public class AirportController : ApiControllerBase
    {
        [HttpGet]
        public async Task<AirportVM> Get()
        {
            return await Mediator.Send(new GetAllAirpotsQuery());
            
        }
    }
}
