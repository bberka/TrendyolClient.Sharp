using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolApprovedProductContent
{
    [JsonPropertyName("contentId")]
    public long ContentId { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("brand")]
    public TrendyolBrand Brand { get; set; }

    [JsonPropertyName("category")]
    public TrendyolCategory Category { get; set; }

    [JsonPropertyName("creationDate")]
    public long CreationDate { get; set; }

    [JsonPropertyName("lastModifiedDate")]
    public long LastModifiedDate { get; set; }

    [JsonPropertyName("lastModifiedBy")]
    public string LastModifiedBy { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("images")]
    public List<TrendyolProductImage> Images { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolProductAttributeDetail> Attributes { get; set; }

    [JsonPropertyName("variants")]
    public List<TrendyolApprovedVariant> Variants { get; set; }
}
