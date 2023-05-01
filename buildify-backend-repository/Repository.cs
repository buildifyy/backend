using buildify_backend_models;
using buildify_backend_models.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace buildify_backend_repository;
public class Repository: IRepository
{
    private readonly CosmosClient _client;
    private readonly string _databaseName;

    public Repository(CosmosClient client, string databaseName)
    {
        _client = client;
        _databaseName = databaseName;
    }

    public async Task<IEnumerable<Template>> RetrieveAllTemplates()
    {
        var queryable = _client.GetDatabase(_databaseName).GetContainer("Template").GetItemLinqQueryable<Template>();
        var iterator = queryable.Select(row => row).ToFeedIterator();
        return await iterator.ReadNextAsync();
    }
}

