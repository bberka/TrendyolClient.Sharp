using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportRequestGetCategoriesByBarcodes
{
    [JsonPropertyName("barcodes")]
    public List<string> Barcodes { get; set; }
}
