using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IWeatherService weatherService;

        public WeatherForecastController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Get(string city)
        {
            return Ok(weatherService.GetWeather(city));
        }
    }
}
