using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportCareInstruction
{
    /// <summary>
    ///     Care instruction type name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Care instruction code to use in requests
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }
}
