using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportBarcodeCategory
{
    /// <summary>
    ///     Category ID
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }
}
