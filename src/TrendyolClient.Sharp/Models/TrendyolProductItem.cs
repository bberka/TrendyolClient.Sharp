using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolProductItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("brandId")]
    public long BrandId { get; set; }

    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("dimensionalWeight")]
    public double DimensionalWeight { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("currencyType")]
    public string CurrencyType { get; set; }

    [JsonPropertyName("listPrice")]
    public double ListPrice { get; set; }

    [JsonPropertyName("salePrice")]
    public double SalePrice { get; set; }

    [JsonPropertyName("vatRate")]
    public int VatRate { get; set; }

    [JsonPropertyName("cargoCompanyId")]
    public int CargoCompanyId { get; set; }

    [JsonPropertyName("images")]
    public List<TrendyolProductImage> Images { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolProductAttribute> Attributes { get; set; }
}