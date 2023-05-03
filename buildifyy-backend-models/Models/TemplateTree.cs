using System;
using buildifyy_backend_models.Models;
using Newtonsoft.Json;

namespace buildify_backend_models.Models
{
	public class TemplateTree
	{
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("children")]
        public List<TemplateTree> Children { get; set; }
    }
}

