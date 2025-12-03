using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestDeleteProduct
{
    [JsonPropertyName("items")]
    public List<TrendyolDeleteProductItem> Items { get; set; }
}