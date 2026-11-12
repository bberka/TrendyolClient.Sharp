using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolMultiSplitResult
{
    [JsonPropertyName("newPackageId")]
    public long NewPackageId { get; set; }
}
