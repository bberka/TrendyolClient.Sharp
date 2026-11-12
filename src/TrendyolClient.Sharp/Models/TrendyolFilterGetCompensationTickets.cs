using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetCompensationTickets
{
    /// <summary>Timestamp (ms, GMT+3).</summary>
    [JsonPropertyName("startDate")]
    public long? StartDate { get; set; }

    /// <summary>Timestamp (ms, GMT+3).</summary>
    [JsonPropertyName("endDate")]
    public long? EndDate { get; set; }

    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>Default 10, max 100.</summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }
}
