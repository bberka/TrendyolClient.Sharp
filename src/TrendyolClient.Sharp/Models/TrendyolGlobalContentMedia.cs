using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalContentMedia
{
    [JsonPropertyName("content_id")]
    public string ContentId { get; set; }

    /// <summary>
    ///     Size chart image URLs keyed by language code (de, en, tr, ...)
    /// </summary>
    [JsonPropertyName("sizeCharts")]
    public Dictionary<string, string> SizeCharts { get; set; }

    [JsonPropertyName("medias")]
    public List<TrendyolGlobalMedia> Medias { get; set; }
}
