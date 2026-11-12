using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolAnswer
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    /// <summary>Timestamp (ms).</summary>
    [JsonPropertyName("creationDate")]
    public long CreationDate { get; set; }

    [JsonPropertyName("hasPrivateInfo")]
    public bool HasPrivateInfo { get; set; }

    /// <summary>Rejection reason (rejectedAnswer only).</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
}
