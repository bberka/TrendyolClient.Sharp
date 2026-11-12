using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestCreateVideo
{
    /// <summary>
    ///     3-50 chars.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    ///     Max 500 chars.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     http(s) URL the video is downloaded from.
    /// </summary>
    [JsonPropertyName("videoUrl")]
    public string VideoUrl { get; set; }

    /// <summary>
    ///     Up to 100 content ids; a content can have only one active video.
    /// </summary>
    [JsonPropertyName("productContentIds")]
    public List<string> ProductContentIds { get; set; }

    /// <summary>
    ///     PRODUCT_PROMOTION (default), ASSEMBLY_AND_INSTALLATION, PACKAGING, STORE_PROMOTION, ADVERTISEMENT or PRODUCT_USAGE_AND_EXPERIENCE.
    /// </summary>
    [JsonPropertyName("videoContentType")]
    public string VideoContentType { get; set; }
}
