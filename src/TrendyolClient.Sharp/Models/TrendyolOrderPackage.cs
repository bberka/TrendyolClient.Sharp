using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Shipment package as returned by the order packages services and as posted to webhooks.
/// </summary>
public class TrendyolOrderPackage
{
    /// <summary>
    ///     Package id (the id used by every package operation).
    /// </summary>
    [JsonPropertyName("shipmentPackageId")]
    public long ShipmentPackageId { get; set; }

    /// <summary>
    ///     Main Trendyol order number; shared by all packages of an order.
    /// </summary>
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>
    ///     Delivery number.
    /// </summary>
    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("shipmentNumber")]
    public string ShipmentNumber { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("shipmentPackageStatus")]
    public string ShipmentPackageStatus { get; set; }

    /// <summary>
    ///     Payment method to show on the invoice: "Banka Kartı", "Kredi Kartı", "Alışveriş Kredisi", "Cüzdan - Trendpay", "Şimdi Al Sonra Öde - Trendpay" or "-".
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public string PaymentMethod { get; set; }

    /// <summary>
    ///     Country the customer ordered from.
    /// </summary>
    [JsonPropertyName("orderCountryCode")]
    public string OrderCountryCode { get; set; }

    /// <summary>
    ///     1 for the TR core channel, 25 for Trendyol Luxe.
    /// </summary>
    [JsonPropertyName("channelId")]
    public int? ChannelId { get; set; }

    /// <summary>
    ///     "order-creation", "cancel", "split" or "transfer".
    /// </summary>
    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; }

    /// <summary>
    ///     Set after cancel/split operations; the id of the original package.
    /// </summary>
    [JsonPropertyName("originPackageIds")]
    public List<long> OriginPackageIds { get; set; }

    /// <summary>
    ///     Milliseconds, GMT+3.
    /// </summary>
    [JsonPropertyName("orderDate")]
    public long OrderDate { get; set; }

    [JsonPropertyName("lastModifiedDate")]
    public long? LastModifiedDate { get; set; }

    /// <summary>
    ///     Gross package amount before discounts.
    /// </summary>
    [JsonPropertyName("packageGrossAmount")]
    public decimal PackageGrossAmount { get; set; }

    [JsonPropertyName("packageSellerDiscount")]
    public decimal PackageSellerDiscount { get; set; }

    /// <summary>
    ///     Only non-zero when <c>commercial</c> is true.
    /// </summary>
    [JsonPropertyName("packageTyDiscount")]
    public decimal PackageTyDiscount { get; set; }

    /// <summary>
    ///     packageSellerDiscount + packageTyDiscount.
    /// </summary>
    [JsonPropertyName("packageTotalDiscount")]
    public decimal PackageTotalDiscount { get; set; }

    /// <summary>
    ///     Net package amount; the amount to invoice.
    /// </summary>
    [JsonPropertyName("packageTotalPrice")]
    public decimal PackageTotalPrice { get; set; }

    [JsonPropertyName("discountDisplays")]
    public List<TrendyolDiscountDisplay> DiscountDisplays { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }

    /// <summary>
    ///     May be empty for micro export and overseas intermediary orders; fetch again to get it filled.
    /// </summary>
    [JsonPropertyName("shipmentAddress")]
    public TrendyolOrderAddress ShipmentAddress { get; set; }

    /// <summary>
    ///     Holds Trendyol company details for overseas intermediary (is4P) orders.
    /// </summary>
    [JsonPropertyName("invoiceAddress")]
    public TrendyolOrderAddress InvoiceAddress { get; set; }

    /// <summary>
    ///     "Shipment" or "CollectionPoint".
    /// </summary>
    [JsonPropertyName("deliveryAddressType")]
    public string DeliveryAddressType { get; set; }

    [JsonPropertyName("customerId")]
    public long? CustomerId { get; set; }

    [JsonPropertyName("customerFirstName")]
    public string CustomerFirstName { get; set; }

    [JsonPropertyName("customerLastName")]
    public string CustomerLastName { get; set; }

    [JsonPropertyName("customerEmail")]
    public string CustomerEmail { get; set; }

    [JsonPropertyName("supplierId")]
    public long? SupplierId { get; set; }

    [JsonPropertyName("taxNumber")]
    public string TaxNumber { get; set; }

    /// <summary>
    ///     National id for gold, fertilizer and orders above 5000 TRY.
    /// </summary>
    [JsonPropertyName("identityNumber")]
    public string IdentityNumber { get; set; }

    /// <summary>
    ///     True for corporate-invoice orders.
    /// </summary>
    [JsonPropertyName("commercial")]
    public bool Commercial { get; set; }

    [JsonPropertyName("lines")]
    public List<TrendyolPackageLine> Lines { get; set; }

    [JsonPropertyName("packageHistories")]
    public List<TrendyolPackageHistory> PackageHistories { get; set; }

    [JsonPropertyName("cargoProviderName")]
    public string CargoProviderName { get; set; }

    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("cargoTrackingNumber")]
    public string CargoTrackingNumber { get; set; }

    [JsonPropertyName("cargoTrackingLink")]
    public string CargoTrackingLink { get; set; }

    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("cargoSenderNumber")]
    public string CargoSenderNumber { get; set; }

    [JsonPropertyName("sellerDeliveryMethod")]
    public string SellerDeliveryMethod { get; set; }

    [JsonPropertyName("sellerOtpCode")]
    public string SellerOtpCode { get; set; }

    /// <summary>
    ///     1 when the seller pays shipping; absent for Trendyol agreements.
    /// </summary>
    [JsonPropertyName("whoPays")]
    public int? WhoPays { get; set; }

    [JsonPropertyName("deliveryType")]
    public string DeliveryType { get; set; }

    [JsonPropertyName("timeSlotId")]
    public long? TimeSlotId { get; set; }

    [JsonPropertyName("estimatedDeliveryStartDate")]
    public long? EstimatedDeliveryStartDate { get; set; }

    [JsonPropertyName("estimatedDeliveryEndDate")]
    public long? EstimatedDeliveryEndDate { get; set; }

    [JsonPropertyName("agreedDeliveryDate")]
    public long? AgreedDeliveryDate { get; set; }

    [JsonPropertyName("originShipmentDate")]
    public long? OriginShipmentDate { get; set; }

    [JsonPropertyName("fastDelivery")]
    public bool FastDelivery { get; set; }

    /// <summary>
    ///     "TodayDelivery", "SameDayShipping" or "FastDelivery".
    /// </summary>
    [JsonPropertyName("fastDeliveryType")]
    public string FastDeliveryType { get; set; }

    [JsonPropertyName("deliveredByService")]
    public bool DeliveredByService { get; set; }

    [JsonPropertyName("warehouseId")]
    public long? WarehouseId { get; set; }

    [JsonPropertyName("invoiceLink")]
    public string InvoiceLink { get; set; }

    [JsonPropertyName("invoiceNumber")]
    public string InvoiceNumber { get; set; }

    /// <summary>
    ///     "NotInvoiced", "Received", "Rejected" or "Invoiced".
    /// </summary>
    [JsonPropertyName("invoiceStatus")]
    public string InvoiceStatus { get; set; }

    /// <summary>
    ///     Returned for is4P orders with invoiceStatus "Rejected".
    /// </summary>
    [JsonConverter(typeof(TrendyolStringListConverter))]
    [JsonPropertyName("invoiceRejectedReasonKeys")]
    public List<string> InvoiceRejectedReasonKeys { get; set; }

    /// <summary>
    ///     True for micro export orders.
    /// </summary>
    [JsonPropertyName("micro")]
    public bool Micro { get; set; }

    [JsonPropertyName("giftBoxRequested")]
    public bool GiftBoxRequested { get; set; }

    /// <summary>
    ///     Trendyol Export Partnership orders; <c>micro</c> is false when true.
    /// </summary>
    [JsonPropertyName("3pByTrendyol")]
    public bool ThreePByTrendyol { get; set; }

    /// <summary>
    ///     True for Trendyol overseas intermediary orders.
    /// </summary>
    [JsonPropertyName("is4P")]
    public bool Is4P { get; set; }

    /// <summary>
    ///     Returned when <c>micro</c> is true.
    /// </summary>
    [JsonPropertyName("etgbNo")]
    public string EtgbNo { get; set; }

    /// <summary>
    ///     Returned when <c>micro</c> is true.
    /// </summary>
    [JsonPropertyName("etgbDate")]
    public long? EtgbDate { get; set; }

    /// <summary>
    ///     Micro export packages that contain batteries, perfume and the like.
    /// </summary>
    [JsonPropertyName("containsDangerousProduct")]
    public bool ContainsDangerousProduct { get; set; }

    [JsonPropertyName("hsCode")]
    public string HsCode { get; set; }

    /// <summary>
    ///     May change after the package is created.
    /// </summary>
    [JsonPropertyName("cargoDeci")]
    public decimal? CargoDeci { get; set; }

    [JsonPropertyName("boxQuantity")]
    public int? BoxQuantity { get; set; }

    [JsonPropertyName("deci")]
    public decimal? Deci { get; set; }

    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("width")]
    public string Width { get; set; }

    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("length")]
    public string Length { get; set; }

    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("height")]
    public string Height { get; set; }

    [JsonPropertyName("isCod")]
    public bool IsCod { get; set; }
}
