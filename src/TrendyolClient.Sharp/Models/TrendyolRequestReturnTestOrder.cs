using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestReturnTestOrder
{
    [JsonPropertyName("shipmentPackageId")]
    public long ShipmentPackageId { get; set; }
}