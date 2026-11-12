using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetVideos
{
    [JsonPropertyName("meta")]
    public TrendyolVideoMeta Meta { get; set; }

    [JsonPropertyName("data")]
    public List<TrendyolVideo> Data { get; set; }
}
