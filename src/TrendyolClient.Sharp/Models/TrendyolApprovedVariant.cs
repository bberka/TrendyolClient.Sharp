using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolApprovedVariant
{
    [JsonPropertyName("variantId")]
    public long VariantId { get; set; }

    [JsonPropertyName("supplierId")]
    public long SupplierId { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("commission")]
    public decimal? Commission { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolProductAttributeDetail> Attributes { get; set; }

    [JsonPropertyName("productUrl")]
    public string ProductUrl { get; set; }

    [JsonPropertyName("onSale")]
    public bool OnSale { get; set; }

    /// <summary>
    ///     CORE and/or LUXE.
    /// </summary>
    [JsonPropertyName("channels")]
    public List<string> Channels { get; set; }

    [JsonPropertyName("deliveryOption")]
    public TrendyolProductDeliveryOption DeliveryOption { get; set; }

    [JsonPropertyName("stock")]
    public TrendyolVariantStock Stock { get; set; }

    [JsonPropertyName("price")]
    public TrendyolVariantPrice Price { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("vatRate")]
    public int VatRate { get; set; }

    [JsonPropertyName("sellerCreatedDate")]
    public long SellerCreatedDate { get; set; }

    [JsonPropertyName("sellerModifiedDate")]
    public long SellerModifiedDate { get; set; }

    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    [JsonPropertyName("lockReason")]
    public string LockReason { get; set; }

    [JsonPropertyName("lockDate")]
    public long? LockDate { get; set; }

    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    [JsonPropertyName("archivedDate")]
    public long? ArchivedDate { get; set; }

    [JsonPropertyName("docNeeded")]
    public bool DocNeeded { get; set; }

    [JsonPropertyName("hasViolation")]
    public bool HasViolation { get; set; }

    [JsonPropertyName("blacklisted")]
    public bool Blacklisted { get; set; }

    [JsonPropertyName("cargoProviders")]
    public List<string> CargoProviders { get; set; }
}
