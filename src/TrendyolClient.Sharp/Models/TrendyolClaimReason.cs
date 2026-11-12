using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Return reason. See <see cref="TrendyolClaimIssueReason"/> and the TrendyolReturnReason enum for the id values.</summary>
public class TrendyolClaimReason
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("externalReasonId")]
    public int ExternalReasonId { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }
}
