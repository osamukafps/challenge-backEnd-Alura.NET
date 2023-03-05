using AluraFlixChallenge.API.Context;
using AluraFlixChallenge.API.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AluraFlixChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {   
            MongoDbContext _context = new MongoDbContext();

            var videos = _context.Videos.Find<Video>(x => x.Id == 1)
                                        .Project<Video>(Builders<Video>.Projection.Exclude("_id"))
                                        .ToList();

            var listaDeVideos = _context.Videos.Find(x => true).ToList();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpGet(Name = "GetVideos")]
        //public IActionResult GetVideos()
        //{
        //    MongoDbContext _context = new MongoDbContext();

        //    try
        //    {
        //        if (_context is null)
        //            return BadRequest();

        //        return Ok(_context.Videos);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}