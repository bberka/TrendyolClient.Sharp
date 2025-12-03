using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetProducts
{
    [JsonPropertyName("approved")]
    public bool? Approved { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("startDate")]
    public long? StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public long? EndDate { get; set; }

    [JsonPropertyName("page")]
    public int? Page { get; set; }

    [JsonPropertyName("size")]
    public int? Size { get; set; }

    [JsonPropertyName("dateQueryType")]
    public string DateQueryType { get; set; }

    [JsonPropertyName("supplierId")]
    public long? SupplierId { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("archived")]
    public bool? Archived { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("onSale")]
    public bool? OnSale { get; set; }

    [JsonPropertyName("rejected")]
    public bool? Rejected { get; set; }

    [JsonPropertyName("blacklisted")]
    public bool? Blacklisted { get; set; }

    [JsonPropertyName("brandIds")]
    public List<long> BrandIds { get; set; }
}