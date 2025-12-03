using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateProduct
{
    [JsonPropertyName("items")]
    public List<TrendyolProductItem> Items { get; set; }
}