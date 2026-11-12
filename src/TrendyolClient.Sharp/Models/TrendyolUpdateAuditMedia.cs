using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolUpdateAuditMedia
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }
}
