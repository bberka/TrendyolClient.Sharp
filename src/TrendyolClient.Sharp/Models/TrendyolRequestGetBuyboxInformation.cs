using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestGetBuyboxInformation
{
    /// <summary>
    ///     Up to 10 barcodes.
    /// </summary>
    [JsonPropertyName("barcodes")]
    public List<string> Barcodes { get; set; }
}
