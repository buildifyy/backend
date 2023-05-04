using buildify_backend_models.Models;
using buildifyy_backend_models.Models;
using buildifyy_backend_service;
using Microsoft.AspNetCore.Mvc;

namespace buildifyy_backend.Controllers;

[ApiController]
[Route("/templates")]
public class TemplateController : ControllerBase
{
    private readonly IService _service;

    public TemplateController(IService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetTemplates")]
    public async Task<object> GetTemplates([FromQuery] string? style)
    {
        if (string.IsNullOrEmpty(style) || !style.ToLowerInvariant().Equals("tree"))
        {
            return await _service.FetchTemplatesFlat();
        }
        return await _service.FetchTemplatesHierarchy();
    }

    [HttpGet("{templateId}", Name = "GetTemplateById")]
    public async Task<Template> GetTemplateById([FromRoute] string templateId)
    {
        return await _service.FetchTemplate(templateId);
    }

    [HttpPost(Name = "CreateTemplate")]
    public async Task CreateTemplate([FromBody] CreateTemplateDTO templateToCreate)
    {
        await _service.CreateTemplate(templateToCreate);
    }

    [HttpGet("{templateId}/children", Name = "GetTemplateChildren")]
    public async Task<List<Template>> GetTemplateChildren([FromRoute] string templateId)
    {
        return await _service.FetchTemplateChildren(templateId);
    }
}

