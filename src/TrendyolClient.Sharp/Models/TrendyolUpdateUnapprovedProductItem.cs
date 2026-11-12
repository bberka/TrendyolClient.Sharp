using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Item of an unapproved product bulk update. All fields required to create the product must be sent.
/// </summary>
public class TrendyolUpdateUnapprovedProductItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("brandId")]
    public long? BrandId { get; set; }

    [JsonPropertyName("categoryId")]
    public long? CategoryId { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("dimensionalWeight")]
    public decimal? DimensionalWeight { get; set; }

    [JsonPropertyName("vatRate")]
    public int? VatRate { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("deliveryOption")]
    public TrendyolProductDeliveryOption DeliveryOption { get; set; }

    /// <summary>
    ///     ENABLED, DISABLED or null.
    /// </summary>
    [JsonPropertyName("locationBasedDelivery")]
    public string LocationBasedDelivery { get; set; }

    [JsonPropertyName("lotNumber")]
    public string LotNumber { get; set; }

    [JsonPropertyName("cargoProviders")]
    public List<string> CargoProviders { get; set; }

    [JsonPropertyName("shipmentAddressId")]
    public long? ShipmentAddressId { get; set; }

    [JsonPropertyName("returningAddressId")]
    public long? ReturningAddressId { get; set; }

    [JsonPropertyName("images")]
    public List<TrendyolProductImage> Images { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolProductAttribute> Attributes { get; set; }
}
