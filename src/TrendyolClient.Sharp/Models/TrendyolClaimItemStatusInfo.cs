using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimItemStatusInfo
{
    /// <summary>Claim item status name, see TrendyolClaimItemStatus.</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
