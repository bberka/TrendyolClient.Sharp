using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateWarehouse
{
    [JsonPropertyName("warehouseId")]
    public long WarehouseId { get; set; }
}
