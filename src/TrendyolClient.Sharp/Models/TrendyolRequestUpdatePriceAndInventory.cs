using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdatePriceAndInventory
{
    [JsonPropertyName("items")]
    public List<TrendyolPriceAndInventoryItem> Items { get; set; }
}