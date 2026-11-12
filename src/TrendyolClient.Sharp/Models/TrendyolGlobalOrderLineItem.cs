using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalOrderLineItem
{
    [JsonPropertyName("cancelable")]
    public bool Cancelable { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("warehouseType")]
    public string WarehouseType { get; set; }
}
