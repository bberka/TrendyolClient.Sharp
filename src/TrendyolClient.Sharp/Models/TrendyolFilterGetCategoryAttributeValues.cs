using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetCategoryAttributeValues
{
    /// <summary>
    ///     Max 1000.
    /// </summary>
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>
    ///     Max 1000.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }
}
