using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Item of a V2 createProducts request.
/// </summary>
public class TrendyolCreateProductItem
{
    /// <summary>
    ///     Max 40 chars. Only ".", "-" and "_" are allowed as special characters.
    /// </summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Max 100 chars.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    ///     Seller-defined model code that groups variants. Max 40 chars.
    /// </summary>
    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("brandId")]
    public long BrandId { get; set; }

    /// <summary>
    ///     Must be a leaf category.
    /// </summary>
    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>
    ///     Max 100 chars.
    /// </summary>
    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("dimensionalWeight")]
    public decimal? DimensionalWeight { get; set; }

    /// <summary>
    ///     HTML allowed, max 30000 chars.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Two letter country code of origin (see the origin values list). Optional for now; send the origin attribute as well where the category requires it.
    /// </summary>
    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    /// <summary>
    ///     Must not be lower than salePrice.
    /// </summary>
    [JsonPropertyName("listPrice")]
    public decimal ListPrice { get; set; }

    [JsonPropertyName("salePrice")]
    public decimal SalePrice { get; set; }

    [JsonPropertyName("vatRate")]
    public int VatRate { get; set; }

    [JsonPropertyName("deliveryOption")]
    public TrendyolProductDeliveryOption DeliveryOption { get; set; }

    /// <summary>
    ///     Up to 8 images.
    /// </summary>
    [JsonPropertyName("images")]
    public List<TrendyolProductImage> Images { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolProductAttribute> Attributes { get; set; }

    /// <summary>
    ///     Batch/lot/expiry information. Max 100 chars (A-Z, a-z, 0-9, ",", "-", ".", ":", "/").
    /// </summary>
    [JsonPropertyName("lotNumber")]
    public string LotNumber { get; set; }

    [JsonPropertyName("shipmentAddressId")]
    public long? ShipmentAddressId { get; set; }

    [JsonPropertyName("returningAddressId")]
    public long? ReturningAddressId { get; set; }

    /// <summary>
    ///     Barcode level cargo provider codes from getCargoProviders.
    /// </summary>
    [JsonPropertyName("cargoProviders")]
    public List<string> CargoProviders { get; set; }
}
