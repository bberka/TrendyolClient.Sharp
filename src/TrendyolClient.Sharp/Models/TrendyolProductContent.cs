using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolProductContent
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("approved")]
    public bool Approved { get; set; }

    [JsonPropertyName("productCode")]
    public long ProductCode { get; set; }

    [JsonPropertyName("batchRequestId")]
    public string BatchRequestId { get; set; }

    [JsonPropertyName("supplierId")]
    public long SupplierId { get; set; }

    [JsonPropertyName("createDateTime")]
    public long CreateDateTime { get; set; }

    [JsonPropertyName("lastUpdateDate")]
    public long LastUpdateDate { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("brand")]
    public string Brand { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("stockUnitType")]
    public string StockUnitType { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("listPrice")]
    public decimal ListPrice { get; set; }

    [JsonPropertyName("salePrice")]
    public decimal SalePrice { get; set; }

    [JsonPropertyName("vatRate")]
    public decimal VatRate { get; set; }

    [JsonPropertyName("dimensionalWeight")]
    public decimal DimensionalWeight { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("locationBasedDelivery")]
    public string LocationBasedDelivery { get; set; }

    [JsonPropertyName("lotNumber")]
    public string LotNumber { get; set; }

    [JsonPropertyName("deliveryOption")]
    public TrendyolProductDeliveryOption DeliveryOption { get; set; }

    [JsonPropertyName("images")]
    public List<TrendyolProductImage> Images { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolProductAttribute> Attributes { get; set; }

    [JsonPropertyName("variantAttributes")]
    public List<TrendyolProductAttribute> VariantAttributes { get; set; }

    [JsonPropertyName("platformListingId")]
    public string PlatformListingId { get; set; }

    [JsonPropertyName("stockId")]
    public string StockId { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("productUrl")]
    public string ProductUrl { get; set; }
}