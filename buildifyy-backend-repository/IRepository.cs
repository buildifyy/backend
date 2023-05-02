using System;
using buildify_backend_models.Models;
using buildifyy_backend_models;
using buildifyy_backend_models.Models;

namespace buildifyy_backend_repository
{
	public interface IRepository
	{
        Task<IEnumerable<Template>> RetrieveAllTemplates(string? parentId);
        Task CreateTemplate(CreateTemplateDTO templateToCreate);
        Task<IEnumerable<Template>> GetChildTemplates(string parentId);
    }		
}	

