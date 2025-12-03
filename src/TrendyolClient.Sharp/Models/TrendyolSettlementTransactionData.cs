using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Settlement transaction data (Sales, Returns, Discounts, Coupons, Provisions)
/// </summary>
public class TrendyolSettlementTransactionData
{
    /// <summary>
    ///     Unique transaction ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    ///     Transaction date (timestamp in milliseconds)
    /// </summary>
    [JsonPropertyName("transactionDate")]
    public long TransactionDate { get; set; }

    /// <summary>
    ///     Product barcode
    /// </summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Type of financial transaction
    /// </summary>
    [JsonPropertyName("transactionType")]
    public TrendyolSettlementsTransactionType TransactionType { get; set; }

    /// <summary>
    ///     Receipt ID
    /// </summary>
    [JsonPropertyName("receiptId")]
    public long? ReceiptId { get; set; }

    /// <summary>
    ///     Transaction description
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Debt amount
    /// </summary>
    [JsonPropertyName("debt")]
    public double Debt { get; set; }

    /// <summary>
    ///     Credit amount
    /// </summary>
    [JsonPropertyName("credit")]
    public double Credit { get; set; }

    /// <summary>
    ///     Payment period in days
    /// </summary>
    [JsonPropertyName("paymentPeriod")]
    public long? PaymentPeriod { get; set; }

    /// <summary>
    ///     Commission rate percentage
    /// </summary>
    [JsonPropertyName("commissionRate")]
    public double? CommissionRate { get; set; }

    /// <summary>
    ///     Commission amount
    /// </summary>
    [JsonPropertyName("commissionAmount")]
    public double? CommissionAmount { get; set; }

    /// <summary>
    ///     Seller revenue amount
    /// </summary>
    [JsonPropertyName("sellerRevenue")]
    public double? SellerRevenue { get; set; }

    /// <summary>
    ///     Order number
    /// </summary>
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>
    ///     Payment order ID (formed after order is paid, typically created on Wednesdays)
    /// </summary>
    [JsonPropertyName("paymentOrderId")]
    public long? PaymentOrderId { get; set; }

    /// <summary>
    ///     Payment date (timestamp in milliseconds)
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public long? PaymentDate { get; set; }

    /// <summary>
    ///     Commission invoice serial number
    /// </summary>
    [JsonPropertyName("commissionInvoiceSerialNumber")]
    public string CommissionInvoiceSerialNumber { get; set; }

    /// <summary>
    ///     Seller ID
    /// </summary>
    [JsonPropertyName("sellerId")]
    public long SellerId { get; set; }

    /// <summary>
    ///     Store ID (for Market vendors, null for Marketplace vendors)
    /// </summary>
    [JsonPropertyName("storeId")]
    public string StoreId { get; set; }

    /// <summary>
    ///     Store name
    /// </summary>
    [JsonPropertyName("storeName")]
    public string StoreName { get; set; }

    /// <summary>
    ///     Store address
    /// </summary>
    [JsonPropertyName("storeAddress")]
    public string StoreAddress { get; set; }

    /// <summary>
    ///     Country name
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; }

    /// <summary>
    ///     Order date (timestamp in milliseconds)
    /// </summary>
    [JsonPropertyName("orderDate")]
    public long? OrderDate { get; set; }

    /// <summary>
    ///     Affiliate identifier (e.g., "TRENDYOLTR" or "TRENDYOLAZJV")
    /// </summary>
    [JsonPropertyName("affiliate")]
    public string Affiliate { get; set; }

    /// <summary>
    ///     Shipment package ID
    /// </summary>
    [JsonPropertyName("shipmentPackageId")]
    public long? ShipmentPackageId { get; set; }
}