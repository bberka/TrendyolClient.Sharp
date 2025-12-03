using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolLaborCostItem
{
    [JsonPropertyName("orderLineId")]
    public long OrderLineId { get; set; }

    [JsonPropertyName("laborCostPerItem")]
    public double LaborCostPerItem { get; set; }
}