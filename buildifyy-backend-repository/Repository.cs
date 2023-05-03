using buildify_backend_models.Models;
using buildifyy_backend_models;
using buildifyy_backend_models.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace buildifyy_backend_repository;
public class Repository : IRepository
{
    private readonly CosmosClient _client;
    private readonly string _databaseName;

    public Repository(CosmosClient client, string databaseName)
    {
        _client = client;
        _databaseName = databaseName;
    }

    public async Task CreateTemplate(CreateTemplateDTO templateToCreate)
    {
        var templateContainer = _client.GetContainer(_databaseName, "Template");
        var parentTemplateResponse = await templateContainer.ReadItemAsync<Template>(templateToCreate.ParentId, new PartitionKey(templateToCreate.ParentId));
        var parentTemplate = parentTemplateResponse.Resource;
        var hierarchyPath = string.Empty;
        if (string.IsNullOrEmpty(parentTemplate.HierarchyPath))
        {
            hierarchyPath = $"{templateToCreate.ParentId}$";
        }
        else
        {
            hierarchyPath = $"{parentTemplate.HierarchyPath}{templateToCreate.ParentId}$";
        }
        var templateAttributesToCreate = parentTemplate.Attributes;
        foreach (var attribute in templateToCreate.Attributes)
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
        foreach (var relationship in templateToCreate.Relationships)
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
            Name = templateToCreate.Name,
            ParentId = templateToCreate.ParentId,
            HierarchyPath = hierarchyPath,
            Attributes = templateAttributesToCreate,
            Relationships = templateRelationshipsToCreate
        };
        await templateContainer.CreateItemAsync(template);
    }

    public async Task<IEnumerable<Template>> GetChildTemplates(string parentId)
    {
        var queryable = _client.GetContainer(_databaseName, "Template").GetItemLinqQueryable<Template>();
        var feed = queryable.Where(q => q.HierarchyPath.Contains($"{parentId}$")).ToFeedIterator();

        var results = new List<Template>();

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (var template in response)
            {
                results.Add(template);
            }
        }

        return results;
    }

    public async Task<List<TemplateTree>> GetTemplateTree()
    {
        var templateTrees = new List<TemplateTree>();
        var queryable = _client.GetContainer(_databaseName, "Template").GetItemLinqQueryable<Template>();
        var feed = queryable.Where(q => !q.ParentId.IsDefined()).ToFeedIterator();

        var rootTemplates = new List<Template>();
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
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
                Children = await GetChildren(queryable, rootTemplate.Id)
            });
        }

        return templateTrees;
    }

    private async Task<List<TemplateTree>> GetChildren(IOrderedQueryable<Template> queryable, string id)
    {
        var templates = new List<TemplateTree>();
        var feed = queryable.Where(q => q.ParentId.Equals(id)).ToFeedIterator();

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach(var template in response)
            {
                templates.Add(new TemplateTree
                {
                    Id = template.Id,
                    Name = template.Name,
                    Children = await GetChildren(queryable, template.Id)
                });
            }
        }

        return templates;
    }

    public async Task<IEnumerable<Template>> RetrieveAllTemplates(string? parentId)
    {
        var queryable = _client.GetContainer(_databaseName, "Template").GetItemLinqQueryable<Template>();

        FeedIterator<Template> feed;
        if (string.IsNullOrEmpty(parentId))
        {
            feed = queryable.Select(row => row).ToFeedIterator();
        }
        else
        {
            feed = queryable.Where(q => q.HierarchyPath.Contains($"{parentId}$")).ToFeedIterator();
        }

        var results = new List<Template>();

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (var template in response)
            {
                results.Add(template);
            }
        }
        return results;
    }

    public async Task<Template> RetrieveTemplate(string templateId)
    {
        var templateContainer = _client.GetContainer(_databaseName, "Template");
        var parentTemplateResponse = await templateContainer.ReadItemAsync<Template>(templateId, new PartitionKey(templateId));
        var parentTemplate = parentTemplateResponse.Resource;

        return parentTemplate;
    }
}

