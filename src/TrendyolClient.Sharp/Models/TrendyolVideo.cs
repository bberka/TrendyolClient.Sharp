using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolVideo
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     IN_PROGRESS, SUCCESS or FAILED.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("videoUrl")]
    public string VideoUrl { get; set; }

    [JsonPropertyName("productContentIds")]
    public List<string> ProductContentIds { get; set; }

    /// <summary>
    ///     Only when status is FAILED.
    /// </summary>
    [JsonPropertyName("errorCode")]
    public string ErrorCode { get; set; }

    [JsonPropertyName("optimizedVideoUrl")]
    public string OptimizedVideoUrl { get; set; }

    /// <summary>
    ///     The video is visible to customers only when true.
    /// </summary>
    [JsonPropertyName("isApproved")]
    public bool IsApproved { get; set; }
}
