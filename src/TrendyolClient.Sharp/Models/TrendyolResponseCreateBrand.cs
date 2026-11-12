using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseCreateBrand
{
    [JsonPropertyName("brandId")]
    public long BrandId { get; set; }
}
