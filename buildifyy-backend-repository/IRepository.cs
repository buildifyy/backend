using buildifyy_backend_models.Models;
using Microsoft.Azure.Cosmos;

namespace buildifyy_backend_repository
{
    public interface IRepository
	{
        Task CreateItem<T>(T data);
        Task<Template> GetTemplateById(string id);
        FeedIterator<Template> GetRootTemplateFeed();
        FeedIterator<Template> GetChildTemplateFeed(string id);
        FeedIterator<Template> GetTemplatesFeed();
    }		
}	

