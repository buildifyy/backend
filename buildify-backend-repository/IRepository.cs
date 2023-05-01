using System;
using buildify_backend_models;

namespace buildify_backend_repository
{
	public interface IRepository
	{
        Task<IEnumerable<SampleProduct>> RetrieveAllProducts();
    }		
}	

