using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetApprovedProductsInventoryAndPrice
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Up to 50.
    /// </summary>
    [JsonPropertyName("barcodes")]
    public List<string> Barcodes { get; set; }

    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>
    ///     Max 100.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    /// <summary>
    ///     archived, blacklisted, locked, onSale or notOnSale.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("nextPageToken")]
    public string NextPageToken { get; set; }

    [JsonPropertyName("contentId")]
    public string ContentId { get; set; }

    /// <summary>
    ///     asc or desc, by seller created date.
    /// </summary>
    [JsonPropertyName("orderByDirection")]
    public string OrderByDirection { get; set; }
}
