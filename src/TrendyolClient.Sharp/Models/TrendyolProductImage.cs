using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolProductImage
{
    /// <summary>
    ///     Public HTTPS URL of the image (up to 8 per product, 1200x1800 recommended).
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }
}
