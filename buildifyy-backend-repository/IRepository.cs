using System;
using buildifyy_backend_models;
using buildifyy_backend_models.Models;

namespace buildifyy_backend_repository
{
	public interface IRepository
	{
        Task<IEnumerable<Template>> RetrieveAllTemplates();
    }		
}	

