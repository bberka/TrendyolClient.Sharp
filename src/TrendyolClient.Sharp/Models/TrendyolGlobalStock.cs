using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalStock
{
    [JsonPropertyName("variantId")]
    public string VariantId { get; set; }

    /// <summary>
    ///     Total available stock
    /// </summary>
    [JsonPropertyName("availableQuantity")]
    public int AvailableQuantity { get; set; }

    [JsonPropertyName("warehouses")]
    public TrendyolGlobalStockWarehouses Warehouses { get; set; }
}
