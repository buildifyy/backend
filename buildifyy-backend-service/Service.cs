using buildify_backend_models.Models;
using buildifyy_backend_models.Models;
using buildifyy_backend_repository;
using Microsoft.Extensions.Logging;

namespace buildifyy_backend_service;
public class Service : IService
{
    private readonly IRepository _repository;
    private readonly ILogger<IService> _logger;

    public Service(ILogger<IService> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task CreateTemplate(CreateTemplateDTO createTemplateDTO)
    {
        var parentTemplate = await _repository.GetTemplateById(createTemplateDTO.ParentId);

        var templateAttributesToCreate = parentTemplate.Attributes;
        foreach (var attribute in createTemplateDTO.Attributes)
        {
            templateAttributesToCreate.Add(new TemplateAttribute
            {
                Id = Guid.NewGuid().ToString(),
                Name = attribute.Name,
                DataType = attribute.DataType,
                IsRequired = attribute.IsRequired
            });
        }
        var templateRelationshipsToCreate = parentTemplate.Relationships;
        foreach (var relationship in createTemplateDTO.Relationships)
        {
            templateRelationshipsToCreate.Add(new TemplateRelationship
            {
                Id = Guid.NewGuid().ToString(),
                Name = relationship.Name,
                ReverseName = relationship.ReverseName,
                Domain = relationship.Domain,
                Range = relationship.Range
            });
        }

        var template = new Template
        {
            Id = Guid.NewGuid().ToString(),
            Name = createTemplateDTO.Name,
            ParentId = createTemplateDTO.ParentId,
            Attributes = templateAttributesToCreate,
            Relationships = templateRelationshipsToCreate
        };

        await _repository.CreateItem(template);
    }

    public async Task<List<TemplateTree>> FetchTemplatesHierarchy(int maxLevel)
    {
        int innerLevel = 1;
        var rootTemplateFeed = _repository.GetRootTemplateFeed();
        var templateTrees = new List<TemplateTree>();

        var rootTemplates = new List<Template>();
        while (rootTemplateFeed.HasMoreResults)
        {
            var response = await rootTemplateFeed.ReadNextAsync();
            foreach (var template in response)
            {
                rootTemplates.Add(template);
            }
        }

        foreach (var rootTemplate in rootTemplates)
        {
            templateTrees.Add(new TemplateTree
            {
                Id = rootTemplate.Id,
                Name = rootTemplate.Name,
                Children = await FetchTemplateChildrenTree(rootTemplate.Id, innerLevel, maxLevel)
            });
        }

        return templateTrees;
    }

    public async Task<List<Template>> FetchTemplatesFlat()
    {
        var templatesFeed = _repository.GetTemplatesFeed();
        var results = new List<Template>();

        while (templatesFeed.HasMoreResults)
        {
            var response = await templatesFeed.ReadNextAsync();
            foreach (var template in response)
            {
                results.Add(template);
            }
        }
        return results;
    }

    public async Task<Template> FetchTemplate(string id)
    {
        return await _repository.GetTemplateById(id);
    }

    public async Task<List<Template>> FetchTemplateChildren(string id)
    {
        var childTemplates = new List<Template>();
        var feed = _repository.GetChildTemplateFeed(id);
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (var template in response)
            {
                childTemplates.Add(template);
            }
        }

        return childTemplates;
    }

    private async Task<List<TemplateTree>> FetchTemplateChildrenTree(string id, int innerLevel, int maxLevel)
    {
        var templates = new List<TemplateTree>();
        if (innerLevel++ == maxLevel)
        {
            return templates;
        }
        var feed = _repository.GetChildTemplateFeed(id);

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (var template in response)
            {
                templates.Add(new TemplateTree
                {
                    Id = template.Id,
                    Name = template.Name,
                    Children = await FetchTemplateChildrenTree(template.Id, innerLevel, maxLevel)
                });
            }
        }

        return templates;
    }
}

