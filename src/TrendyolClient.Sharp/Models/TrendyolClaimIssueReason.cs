using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimIssueReason
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("reasonText")]
    public string ReasonText { get; set; }
}