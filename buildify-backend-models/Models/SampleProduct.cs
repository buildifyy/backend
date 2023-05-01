using Newtonsoft.Json;

namespace buildify_backend_models;
public class SampleProduct
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("categoryId")]
    public string CategoryId { get; set; }

    [JsonProperty("categoryName")]
    public string CategoryName { get; set; }

    [JsonProperty("sku")]
    public string Sku { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("price")]
    public double Price { get; set; }
}

