using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetUpdateAudits
{
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    [JsonPropertyName("size")]
    public int? Size { get; set; }

    /// <summary>
    ///     SUCCESS, FAIL or RUNNING.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }
}
