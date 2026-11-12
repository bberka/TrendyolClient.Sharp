using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseSplitShipmentPackage
{
    [JsonPropertyName("newPackageId")]
    public long NewPackageId { get; set; }
}
