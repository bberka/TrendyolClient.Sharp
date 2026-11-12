using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalWarehouseStock
{
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
