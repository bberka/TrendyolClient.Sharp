using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalStockWarehouses
{
    /// <summary>
    ///     Stock in the Trendyol warehouse
    /// </summary>
    [JsonPropertyName("trendyol")]
    public TrendyolGlobalWarehouseStock Trendyol { get; set; }

    /// <summary>
    ///     Stock in the seller warehouse
    /// </summary>
    [JsonPropertyName("seller")]
    public TrendyolGlobalWarehouseStock Seller { get; set; }
}
