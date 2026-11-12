using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolPackageHistory
{
    /// <summary>
    ///     Timestamp in milliseconds.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public long CreatedDate { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}
