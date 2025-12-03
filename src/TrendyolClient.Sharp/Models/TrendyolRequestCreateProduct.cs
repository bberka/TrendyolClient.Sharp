using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestCreateProduct
{
    [JsonPropertyName("items")]
    public List<TrendyolProductItem> Items { get; set; }
}