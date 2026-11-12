using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseCreateVideo
{
    [JsonPropertyName("videoId")]
    public string VideoId { get; set; }
}
