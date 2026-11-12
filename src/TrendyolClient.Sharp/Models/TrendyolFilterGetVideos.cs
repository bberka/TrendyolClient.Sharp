using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetVideos
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    ///     IN_PROGRESS, SUCCESS or FAILED.
    /// </summary>
    [JsonPropertyName("sellerIntegrationStatus")]
    public string SellerIntegrationStatus { get; set; }

    /// <summary>
    ///     Zero based.
    /// </summary>
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    [JsonPropertyName("size")]
    public int? Size { get; set; }
}
