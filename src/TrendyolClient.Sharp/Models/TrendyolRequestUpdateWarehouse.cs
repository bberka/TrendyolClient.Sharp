using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateWarehouse
{
    [JsonPropertyName("warehouseId")]
    public int WarehouseId { get; set; }
}