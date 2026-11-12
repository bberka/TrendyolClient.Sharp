using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Named lookup value (origin or material composition)
/// </summary>
public class TrendyolExportLookupItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
