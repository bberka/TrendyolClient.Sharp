using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Line of a shipment package.
/// </summary>
public class TrendyolPackageLine
{
    /// <summary>
    ///     Order line id.
    /// </summary>
    [JsonPropertyName("lineId")]
    public long LineId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Seller stock code.
    /// </summary>
    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; }

    [JsonPropertyName("contentId")]
    public long? ContentId { get; set; }

    [JsonPropertyName("productCategoryId")]
    public long? ProductCategoryId { get; set; }

    [JsonPropertyName("productSize")]
    public string ProductSize { get; set; }

    [JsonPropertyName("productColor")]
    public string ProductColor { get; set; }

    /// <summary>
    ///     Country of origin; required on invoices of micro export packages.
    /// </summary>
    [JsonPropertyName("productOrigin")]
    public string ProductOrigin { get; set; }

    [JsonPropertyName("sellerId")]
    public long? SellerId { get; set; }

    [JsonPropertyName("salesCampaignId")]
    public long? SalesCampaignId { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }

    /// <summary>
    ///     Gross unit price before discounts.
    /// </summary>
    [JsonPropertyName("lineGrossAmount")]
    public decimal LineGrossAmount { get; set; }

    /// <summary>
    ///     lineSellerDiscount + lineTyDiscount.
    /// </summary>
    [JsonPropertyName("lineTotalDiscount")]
    public decimal LineTotalDiscount { get; set; }

    [JsonPropertyName("lineSellerDiscount")]
    public decimal LineSellerDiscount { get; set; }

    [JsonPropertyName("lineTyDiscount")]
    public decimal LineTyDiscount { get; set; }

    /// <summary>
    ///     Net unit price: lineGrossAmount - lineSellerDiscount - lineTyDiscount.
    /// </summary>
    [JsonPropertyName("lineUnitPrice")]
    public decimal LineUnitPrice { get; set; }

    [JsonPropertyName("discountDetails")]
    public List<TrendyolPackageLineDiscountDetail> DiscountDetails { get; set; }

    [JsonPropertyName("vatRate")]
    public decimal? VatRate { get; set; }

    /// <summary>
    ///     Commission rate.
    /// </summary>
    [JsonPropertyName("commission")]
    public decimal? Commission { get; set; }

    [JsonPropertyName("orderLineItemStatusName")]
    public string OrderLineItemStatusName { get; set; }

    [JsonPropertyName("fastDeliveryOptions")]
    public List<string> FastDeliveryOptions { get; set; }

    /// <summary>
    ///     "Digital Goods" for digital products (customer phone is null and digital codes can be sent).
    /// </summary>
    [JsonPropertyName("businessUnit")]
    public string BusinessUnit { get; set; }

    [JsonPropertyName("cancelledBy")]
    public string CancelledBy { get; set; }

    [JsonPropertyName("cancelReason")]
    public string CancelReason { get; set; }

    [JsonPropertyName("cancelReasonCode")]
    public int? CancelReasonCode { get; set; }

    /// <summary>
    ///     Describes the defect reported by customers on earlier approved returns of this product.
    /// </summary>
    [JsonPropertyName("defectiveClaimListingInsight")]
    public string DefectiveClaimListingInsight { get; set; }

    /// <summary>SGR deposit fee (Romania, non-micro orders).</summary>
    [JsonPropertyName("lineSgrFee")]
    public decimal? LineSgrFee { get; set; }
}
