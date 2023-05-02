using System;
using Newtonsoft.Json;

namespace buildify_backend_models.Models
{
	public class CreateRelationshipDTO
	{
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("reverseName")]
        public string ReverseName { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }
	}
}

