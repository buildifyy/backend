using System;
using Newtonsoft.Json;

namespace buildifyy_backend_models.Models
{
	public class TemplateAttribute
	{
		[JsonProperty("id")]
		public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dataType")]
        public string DataType { get; set; }

        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; }
    }
}

