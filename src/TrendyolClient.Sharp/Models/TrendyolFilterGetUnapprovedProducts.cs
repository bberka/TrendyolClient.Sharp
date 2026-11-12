using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetUnapprovedProducts
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Up to 50.
    /// </summary>
    [JsonPropertyName("barcodes")]
    public List<string> Barcodes { get; set; }

    [JsonPropertyName("startDate")]
    public long? StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public long? EndDate { get; set; }

    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>
    ///     Max 1000; page x size is capped at 10000, use nextPageToken beyond that.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }

    /// <summary>
    ///     CREATED_DATE or LAST_MODIFIED_DATE.
    /// </summary>
    [JsonPropertyName("dateQueryType")]
    public string DateQueryType { get; set; }

    [JsonPropertyName("supplierId")]
    public long? SupplierId { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("brandIds")]
    public List<long> BrandIds { get; set; }

    /// <summary>
    ///     rejected or pendingApproval.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("nextPageToken")]
    public string NextPageToken { get; set; }
}
