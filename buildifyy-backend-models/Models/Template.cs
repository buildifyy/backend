﻿using System;
using Newtonsoft.Json;

namespace buildifyy_backend_models.Models
{
	public class Template
	{
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("attributes")]
        public IList<TemplateAttribute> Attributes { get; set; }

        [JsonProperty("relationships")]
        public IList<TemplateRelationship> Relationships { get; set; }
    }
}

