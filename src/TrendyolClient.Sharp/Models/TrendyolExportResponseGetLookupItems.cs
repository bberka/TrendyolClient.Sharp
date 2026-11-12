using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportResponseGetLookupItems
{
    [JsonPropertyName("items")]
    public List<TrendyolExportLookupItem> Items { get; set; }
}
