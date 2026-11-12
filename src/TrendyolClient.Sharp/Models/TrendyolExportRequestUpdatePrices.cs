using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportRequestUpdatePrices
{
    /// <summary>
    ///     Max 5000 items
    /// </summary>
    [JsonPropertyName("priceInfos")]
    public List<TrendyolExportPriceItem> PriceInfos { get; set; }
}
