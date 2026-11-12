using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalMedia
{
    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }

    /// <summary>
    ///     Image shows a human model
    /// </summary>
    [JsonPropertyName("is_human")]
    public bool IsHuman { get; set; }

    /// <summary>
    ///     Original image URL
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("url_size_2000_x_2000")]
    public string UrlSize2000X2000 { get; set; }

    [JsonPropertyName("url_white_bg")]
    public string UrlWhiteBg { get; set; }

    [JsonPropertyName("url_size_1500_x_2000")]
    public string UrlSize1500X2000 { get; set; }
}
