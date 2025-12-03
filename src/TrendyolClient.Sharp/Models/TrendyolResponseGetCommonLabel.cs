using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetCommonLabel
{
    [JsonPropertyName("labelContent")]
    public string LabelContent { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; }
}