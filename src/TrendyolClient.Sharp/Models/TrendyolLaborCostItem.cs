using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolLaborCostItem
{
    [JsonPropertyName("orderLineId")]
    public long OrderLineId { get; set; }

    [JsonPropertyName("laborCostPerItem")]
    public decimal LaborCostPerItem { get; set; }
}