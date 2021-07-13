using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace sample_application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly WeatherSummary[] Summaries = new[]
        {
            new WeatherSummary { Summary = "Freezing", FaSummaryClass = "fas fa-temperature-low" },
            new WeatherSummary { Summary = "Bracing", FaSummaryClass = "fas fa-wind" }, 
            new WeatherSummary { Summary = "Chilly", FaSummaryClass = "fas fa-water" },
            new WeatherSummary { Summary = "Cool", FaSummaryClass = "fas fa-cloud-sun" },
            new WeatherSummary { Summary = "Mild", FaSummaryClass = "fas fa-cloud-sun" },
            new WeatherSummary { Summary = "Warm", FaSummaryClass = "fas fa-sun" },
            new WeatherSummary { Summary = "Balmy", FaSummaryClass = "fas fa-sun" },
            new WeatherSummary { Summary = "Hot", FaSummaryClass="fas fa-temperature-high" },
            new WeatherSummary { Summary = "Sweltering", FaSummaryClass="fas fa-temperature-high" },
            new WeatherSummary { Summary = "Scorching", FaSummaryClass="fas fa-temperature-high" }
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }
    }
}
