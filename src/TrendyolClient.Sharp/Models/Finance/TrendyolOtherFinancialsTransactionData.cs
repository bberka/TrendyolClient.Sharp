using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models.Finance;

/// <summary>
/// Other financial transaction data (Supplier financing, transfers, payments, invoices)
/// </summary>
public sealed class TrendyolOtherFinancialsTransactionData
{
    /// <summary>
    /// Unique transaction ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// Transaction date (timestamp in milliseconds)
    /// </summary>
    [JsonPropertyName("transactionDate")]
    public long TransactionDate { get; set; }

    /// <summary>
    /// Product barcode (may be null for non-product transactions)
    /// </summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    /// Type of financial transaction
    /// </summary>
    [JsonPropertyName("transactionType")]
    public TrendyolOtherFinancialTransactionType TransactionType { get; set; }

    /// <summary>
    /// Receipt ID (may be null)
    /// </summary>
    [JsonPropertyName("receiptId")]
    public long? ReceiptId { get; set; }

    /// <summary>
    /// Transaction description (may be null)
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// Debt amount
    /// </summary>
    [JsonPropertyName("debt")]
    public double Debt { get; set; }

    /// <summary>
    /// Credit amount
    /// </summary>
    [JsonPropertyName("credit")]
    public double Credit { get; set; }

    /// <summary>
    /// Payment period in days (may be null)
    /// </summary>
    [JsonPropertyName("paymentPeriod")]
    public long? PaymentPeriod { get; set; }

    /// <summary>
    /// Commission rate percentage (may be null)
    /// </summary>
    [JsonPropertyName("commissionRate")]
    public double? CommissionRate { get; set; }

    /// <summary>
    /// Commission amount (may be null)
    /// </summary>
    [JsonPropertyName("commissionAmount")]
    public double? CommissionAmount { get; set; }

    /// <summary>
    /// Seller revenue amount (may be null)
    /// </summary>
    [JsonPropertyName("sellerRevenue")]
    public double? SellerRevenue { get; set; }

    /// <summary>
    /// Order number (may be null)
    /// </summary>
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>
    /// Payment order ID (may be null)
    /// </summary>
    [JsonPropertyName("paymentOrderId")]
    public long? PaymentOrderId { get; set; }

    /// <summary>
    /// Payment date (timestamp in milliseconds, may be null)
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public long? PaymentDate { get; set; }

    /// <summary>
    /// Commission invoice serial number (may be null)
    /// </summary>
    [JsonPropertyName("commissionInvoiceSerialNumber")]
    public string CommissionInvoiceSerialNumber { get; set; }

    /// <summary>
    /// Seller ID
    /// </summary>
    [JsonPropertyName("sellerId")]
    public long SellerId { get; set; }

    /// <summary>
    /// Store ID (for Market vendors, null for Marketplace vendors)
    /// </summary>
    [JsonPropertyName("storeId")]
    public string StoreId { get; set; }

    /// <summary>
    /// Store name (may be null)
    /// </summary>
    [JsonPropertyName("storeName")]
    public string StoreName { get; set; }

    /// <summary>
    /// Store address (may be null)
    /// </summary>
    [JsonPropertyName("storeAddress")]
    public string StoreAddress { get; set; }

    /// <summary>
    /// Country name
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; }

    /// <summary>
    /// Order date (timestamp in milliseconds, may be null)
    /// </summary>
    [JsonPropertyName("orderDate")]
    public long? OrderDate { get; set; }

    /// <summary>
    /// Affiliate identifier (e.g., "TRENDYOLTR" or "TRENDYOLAZJV")
    /// </summary>
    [JsonPropertyName("affiliate")]
    public string Affiliate { get; set; }

    /// <summary>
    /// Shipment package ID (may be null)
    /// </summary>
    [JsonPropertyName("shipmentPackageId")]
    public long? ShipmentPackageId { get; set; }
}