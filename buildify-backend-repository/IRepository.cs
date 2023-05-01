using System;
using buildify_backend_models;
using buildify_backend_models.Models;

namespace buildify_backend_repository
{
	public interface IRepository
	{
        Task<IEnumerable<Template>> RetrieveAllTemplates();
    }		
}	

