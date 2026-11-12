using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateLaborCost
{
    [JsonPropertyName("orderLineId")]
    public long OrderLineId { get; set; }

    /// <summary>
    ///     Labor cost per single item.
    /// </summary>
    [JsonPropertyName("laborCostPerItem")]
    public decimal LaborCostPerItem { get; set; }
}
