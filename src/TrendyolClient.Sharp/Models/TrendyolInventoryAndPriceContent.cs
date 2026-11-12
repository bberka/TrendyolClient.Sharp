using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolInventoryAndPriceContent
{
    [JsonPropertyName("contentId")]
    public long ContentId { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("variants")]
    public List<TrendyolInventoryAndPriceVariant> Variants { get; set; }
}
