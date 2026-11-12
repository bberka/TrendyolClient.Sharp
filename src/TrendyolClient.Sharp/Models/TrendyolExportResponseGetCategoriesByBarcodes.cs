using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportResponseGetCategoriesByBarcodes
{
    /// <summary>
    ///     Barcode to category map
    /// </summary>
    [JsonPropertyName("barcodeCategories")]
    public Dictionary<string, TrendyolExportBarcodeCategory> BarcodeCategories { get; set; }

    /// <summary>
    ///     Barcodes without a category
    /// </summary>
    [JsonPropertyName("notFound")]
    public List<string> NotFound { get; set; }
}
