using buildify_backend_models;
using buildify_backend_models.Models;
using buildify_backend_repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace buildify_backend.Controllers;

[ApiController]
[Route("/templates")]
public class TemplateController : ControllerBase
{
    private readonly ILogger<TemplateController> _logger;
    private readonly IRepository _repository;

    public TemplateController(ILogger<TemplateController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet(Name = "GetTemplates")]
    public async Task<IEnumerable<Template>> GetTemplates()
    {
        return await _repository.RetrieveAllTemplates();
    }
}

