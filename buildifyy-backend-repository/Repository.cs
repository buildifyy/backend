using buildifyy_backend_models.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace buildifyy_backend_repository;
public class Repository : IRepository
{
    private readonly CosmosClient _client;
    private readonly string _databaseName;
    private readonly Container _templateContainer;

    public Repository(CosmosClient client, string databaseName)
    {
        _client = client;
        _databaseName = databaseName;
        _templateContainer = _client.GetContainer(_databaseName, "Template");
    }

    public async Task CreateItem<T>(T data)
    {
        await _templateContainer.CreateItemAsync(data);
    }

    public async Task<Template> GetTemplateById(string id)
    {
        var templateItem = await _templateContainer.ReadItemAsync<Template>(id, new PartitionKey(id));
        return templateItem.Resource;
    }

    public FeedIterator<Template> GetRootTemplateFeed()
    {
        var queryable = _templateContainer.GetItemLinqQueryable<Template>();
        var feed = queryable.Where(q => !q.ParentId.IsDefined()).ToFeedIterator();
        return feed;
    }

    public FeedIterator<Template> GetChildTemplateFeed(string id)
    {
        var queryable = _templateContainer.GetItemLinqQueryable<Template>();
        var feed = queryable.Where(q => q.ParentId.Equals(id)).ToFeedIterator();
        return feed;
    }

    public FeedIterator<Template> GetTemplatesFeed()
    {
        var queryable = _templateContainer.GetItemLinqQueryable<Template>();
        var feed = queryable.Select(row => row).ToFeedIterator();
        return feed;
    }
}

