using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolSplitPackageDetail
{
    [JsonPropertyName("orderLineId")]
    public long OrderLineId { get; set; }

    [JsonPropertyName("quantities")]
    public int Quantities { get; set; }
}