using Microsoft.AspNetCore.Mvc;

namespace Mosaik.idAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MosaikController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<MosaikController> _logger;

        public MosaikController(ILogger<MosaikController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<MosaikItem> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new MosaikItem
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}