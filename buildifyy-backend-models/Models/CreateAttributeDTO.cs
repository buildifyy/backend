using System;
using Newtonsoft.Json;

namespace buildify_backend_models.Models
{
	public class CreateAttributeDTO
	{
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dataType")]
        public string DataType { get; set; }

        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; }
	}
}

