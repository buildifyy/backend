using buildify_backend_models;
using buildify_backend_repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace buildify_backend.Controllers;

[ApiController]
[Route("/products")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IRepository _repository;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet(Name = "GetSampleProducts")]
    public async Task<IEnumerable<SampleProduct>> GetSampleProducts()
    {
        return await _repository.RetrieveAllProducts();
    }
}

