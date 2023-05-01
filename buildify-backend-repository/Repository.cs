using buildify_backend_models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace buildify_backend_repository;
public class Repository: IRepository
{
    private readonly CosmosClient _client;
    private Container container
    {
        get => _client.GetDatabase("SampleDB").GetContainer("SampleContainer");
    }

    public Repository()
    {
        _client = new CosmosClient(connectionString: "AccountEndpoint=https://buildify-db.documents.azure.com:443/;AccountKey=cpChf1wL8jHPuhLFIZdYhX9FUTavBkoC3PurQFeYxum5aPoc4RDg1SLnhn7ce0cQKVOPnsiVl6KPACDbWCYW3Q==;");
    }

    public async Task<IEnumerable<SampleProduct>> RetrieveAllProducts()
    {
        var queryable = container.GetItemLinqQueryable<SampleProduct>();
        var iterator = queryable.Select(row => row).ToFeedIterator();
        return await iterator.ReadNextAsync();
    }
}

