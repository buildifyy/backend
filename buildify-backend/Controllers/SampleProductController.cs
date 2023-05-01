using buildify_backend_models;
using buildify_backend_repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace buildify_backend.Controllers;

[ApiController]
[Route("/products")]
public class SampleProductController : ControllerBase
{
    private readonly ILogger<SampleProductController> _logger;
    private readonly IRepository _repository;

    public SampleProductController(ILogger<SampleProductController> logger, IRepository repository)
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

