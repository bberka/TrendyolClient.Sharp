using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportPackageItemV3
{
    [JsonPropertyName("itemId")]
    public string ItemId { get; set; }

    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("trackingNumber")]
    public string TrackingNumber { get; set; }

    [JsonPropertyName("packageId")]
    public string PackageId { get; set; }

    [JsonPropertyName("boutiqueId")]
    public int BoutiqueId { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("platformId")]
    public int PlatformId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Barcode in the seller systems
    /// </summary>
    [JsonPropertyName("sellerBarcode")]
    public string SellerBarcode { get; set; }

    /// <summary>
    ///     Barcode expected to be shipped
    /// </summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("newQuantity")]
    public int NewQuantity { get; set; }

    /// <summary>
    ///     Quantity currently awaiting supply
    /// </summary>
    [JsonPropertyName("pendingQuantity")]
    public int PendingQuantity { get; set; }

    /// <summary>
    ///     Quantity accepted by Trendyol
    /// </summary>
    [JsonPropertyName("completedQuantity")]
    public int CompletedQuantity { get; set; }

    /// <summary>
    ///     Quantity that could not be supplied and is no longer expected
    /// </summary>
    [JsonPropertyName("unSuppliedQuantity")]
    public int UnSuppliedQuantity { get; set; }

    [JsonPropertyName("cancelledQuantity")]
    public int CancelledQuantity { get; set; }

    /// <summary>
    ///     Unit price Trendyol buys at, VAT included
    /// </summary>
    [JsonPropertyName("unitBuyingPrice")]
    public decimal UnitBuyingPrice { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    [JsonPropertyName("creationDate")]
    public long CreationDate { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public long LastModifiedDate { get; set; }

    /// <summary>
    ///     new, pending, completed or cancelled
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("cargos")]
    public List<TrendyolExportCargoInfo> Cargos { get; set; }
}
