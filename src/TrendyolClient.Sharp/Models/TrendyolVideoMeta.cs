using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolVideoMeta
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("totalPage")]
    public int TotalPage { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }
}
