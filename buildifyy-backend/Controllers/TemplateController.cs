using buildify_backend_models.Models;
using buildifyy_backend_models;
using buildifyy_backend_models.Models;
using buildifyy_backend_repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace buildifyy_backend.Controllers;

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
    public async Task<IEnumerable<Template>> GetTemplates([FromQuery] string? parentId)
    {
        return await _repository.RetrieveAllTemplates(parentId);
    }

    [HttpGet("{templateId}", Name = "GetTemplate")]
    public async Task<Template> GetTemplate([FromRoute] string templateId)
    {
        return await _repository.RetrieveTemplate(templateId);
    }

    [HttpPost(Name = "CreateTemplate")]
    public async Task CreateTemplate([FromBody] CreateTemplateDTO templateToCreate)
    {
        await _repository.CreateTemplate(templateToCreate);
    }

    [HttpGet("tree", Name = "GetTemplateTree")]
    public async Task<List<TemplateTree>> GetTemplateTree()
    {
        return await _repository.GetTemplateTree();
    }
}

