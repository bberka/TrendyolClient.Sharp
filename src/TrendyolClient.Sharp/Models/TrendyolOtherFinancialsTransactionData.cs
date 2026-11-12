using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Other financial record: stoppage, cash advance, transfers, payment orders, invoices, financial adjustments
/// </summary>
public class TrendyolOtherFinancialsTransactionData
{
    /// <summary>
    ///     Unique transaction ID (for cargo invoices this is the invoiceSerialNumber)
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    ///     Transaction date (unix timestamp, milliseconds)
    /// </summary>
    [JsonPropertyName("transactionDate")]
    public long TransactionDate { get; set; }

    /// <summary>
    ///     Product barcode (null for non-product transactions)
    /// </summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Localized transaction type label returned by the API (e.g. "Satış", "Ödeme"), not the request enum value
    /// </summary>
    [JsonPropertyName("transactionType")]
    public string TransactionType { get; set; }

    /// <summary>
    ///     Receipt number (dekont no)
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
    public decimal Debt { get; set; }

    /// <summary>
    ///     Credit amount
    /// </summary>
    [JsonPropertyName("credit")]
    public decimal Credit { get; set; }

    /// <summary>
    ///     Payment period in days
    /// </summary>
    [JsonPropertyName("paymentPeriod")]
    public long? PaymentPeriod { get; set; }

    /// <summary>
    ///     Commission rate of the ordered product
    /// </summary>
    [JsonPropertyName("commissionRate")]
    public decimal? CommissionRate { get; set; }

    /// <summary>
    ///     Trendyol commission amount
    /// </summary>
    [JsonPropertyName("commissionAmount")]
    public decimal? CommissionAmount { get; set; }

    /// <summary>
    ///     Commission invoice serial number
    /// </summary>
    [JsonPropertyName("commissionInvoiceSerialNumber")]
    public string CommissionInvoiceSerialNumber { get; set; }

    /// <summary>
    ///     Seller revenue amount
    /// </summary>
    [JsonPropertyName("sellerRevenue")]
    public decimal? SellerRevenue { get; set; }

    /// <summary>
    ///     Order number
    /// </summary>
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>
    ///     Payment order ID; created after the order is paid (normally every Wednesday). Matches orders with payments
    /// </summary>
    [JsonPropertyName("paymentOrderId")]
    public long? PaymentOrderId { get; set; }

    /// <summary>
    ///     Payment date (unix timestamp, milliseconds)
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public long? PaymentDate { get; set; }

    /// <summary>
    ///     Seller ID
    /// </summary>
    [JsonPropertyName("sellerId")]
    public long SellerId { get; set; }

    /// <summary>
    ///     Store ID (null for Marketplace sellers)
    /// </summary>
    [JsonPropertyName("storeId")]
    public long? StoreId { get; set; }

    /// <summary>
    ///     Store name (null for Marketplace sellers)
    /// </summary>
    [JsonPropertyName("storeName")]
    public string StoreName { get; set; }

    /// <summary>
    ///     Store address (null for Marketplace sellers)
    /// </summary>
    [JsonPropertyName("storeAddress")]
    public string StoreAddress { get; set; }

    /// <summary>
    ///     Country
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; }

    /// <summary>
    ///     Order date (unix timestamp, milliseconds)
    /// </summary>
    [JsonPropertyName("orderDate")]
    public long? OrderDate { get; set; }

    /// <summary>
    ///     Affiliate code: "TRENDYOLTR" or "TRENDYOLAZJV"
    /// </summary>
    [JsonPropertyName("affiliate")]
    public string Affiliate { get; set; }

    /// <summary>
    ///     Shipment package ID
    /// </summary>
    [JsonPropertyName("shipmentPackageId")]
    public long? ShipmentPackageId { get; set; }
}
