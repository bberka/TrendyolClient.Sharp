using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportCargoInfo
{
    /// <summary>
    ///     Cargo tracking numbers
    /// </summary>
    [JsonPropertyName("codes")]
    public List<string> Codes { get; set; }

    /// <summary>
    ///     Cargo company
    /// </summary>
    [JsonPropertyName("provider")]
    public string Provider { get; set; }
}
