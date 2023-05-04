using buildify_backend_models.Models;
using buildifyy_backend_models.Models;

namespace buildifyy_backend_service
{
    public interface IService
	{
		Task CreateTemplate(CreateTemplateDTO createTemplateDTO);
		Task<List<TemplateTree>> FetchTemplatesHierarchy(int maxLevel);
		Task<List<Template>> FetchTemplatesFlat();
		Task<Template> FetchTemplate(string id);
		Task<List<Template>> FetchTemplateChildren(string id);
    }
}

