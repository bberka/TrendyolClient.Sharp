using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalRequestOrderItem
{
    [JsonPropertyName("variantId")]
    public string VariantId { get; set; }

    /// <summary>
    ///     Must be greater than 0
    /// </summary>
    [JsonPropertyName("requestedQuantity")]
    public int RequestedQuantity { get; set; }

    /// <summary>
    ///     Optional
    /// </summary>
    [JsonPropertyName("warehouseTag")]
    public string WarehouseTag { get; set; }
}
