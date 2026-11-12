using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportRequestUpdateStocks
{
    /// <summary>
    ///     Max 5000 items
    /// </summary>
    [JsonPropertyName("stocks")]
    public List<TrendyolExportStockItem> Stocks { get; set; }
}
