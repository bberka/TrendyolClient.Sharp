using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateLaborCost
{
    [JsonPropertyName("items")]
    public List<TrendyolLaborCostItem> Items { get; set; }
}