using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Create products (V2) request of the Export Center
/// </summary>
public class TrendyolExportRequestCreateProducts
{
    /// <summary>
    ///     Products to open for export (max 5000)
    /// </summary>
    [JsonPropertyName("products")]
    public List<TrendyolExportCreateProductItem> Products { get; set; }
}
