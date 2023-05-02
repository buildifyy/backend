using System;
using Newtonsoft.Json;

namespace buildify_backend_models.Models
{
	public class CreateTemplateDTO
	{
		[JsonProperty("name")]
		public string Name { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("attributes")]
        public List<CreateAttributeDTO> Attributes { get; set; }

        [JsonProperty("relationships")]
        public List<CreateRelationshipDTO> Relationships { get; set; }
	}
}

