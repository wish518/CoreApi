using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;

namespace CoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : BaseController<WeatherForecastController, WeatherForecast, IEnumerable<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
           // _services = services;
        }
        /*
        [HttpGet("{gg}/{SS}")]
        public IEnumerable<WeatherForecast> Get(string gg, string ss)
        {
            string[] parm = new string[] { "gg:"+ gg, ss };
            return base.Get(parm);
        }

        /* [HttpGet("{gg}/{SS}")]
         public IEnumerable<WeatherForecast> Get(string gg, string ss)
         {
             var rng = new Random();
             return Enumerable.Range(1, 5).Select(index => new WeatherForecast
             {
                 Date = DateTime.Now.AddDays(index),
                 TemperatureC = rng.Next(-20, 55),
                 Summary = Summaries[rng.Next(Summaries.Length)]
             })
             .ToArray();
         }*/
    }
}
