using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolShipmentPackage
{
  public TrendyolShipmentAddress ShipmentAddress { get; set; }
  public string OrderNumber { get; set; }
  public decimal GrossAmount { get; set; }
  public decimal TotalDiscount { get; set; }
  public decimal TotalTyDiscount { get; set; }
  public string TaxNumber { get; set; }
  public TrendyolInvoiceAddress InvoiceAddress { get; set; }
  public string CustomerFirstName { get; set; }
  public string CustomerEmail { get; set; }
  public long CustomerId { get; set; }
  public string CustomerLastName { get; set; }
  public long Id { get; set; }
  public long CargoTrackingNumber { get; set; }
  public string CargoTrackingLink { get; set; }
  public string CargoSenderNumber { get; set; }
  public string CargoProviderName { get; set; }
  public List<TrendyolLine> Lines { get; set; }
  public long OrderDate { get; set; }
  public string TcIdentityNumber { get; set; }
  public string CurrencyCode { get; set; }
  public List<TrendyolPackageHistory> PackageHistories { get; set; }
  public string ShipmentPackageStatus { get; set; }
  public string Status { get; set; }
  public string DeliveryType { get; set; }
  public long TimeSlotId { get; set; }
  public string ScheduledDeliveryStoreId { get; set; }
  public long EstimatedDeliveryStartDate { get; set; }
  public long EstimatedDeliveryEndDate { get; set; }
  public decimal TotalPrice { get; set; }
  public string DeliveryAddressType { get; set; }
  public long AgreedDeliveryDate { get; set; }
  public bool AgreedDeliveryDateExtendible { get; set; }
  public long? ExtendedAgreedDeliveryDate { get; set; }
  public long? AgreedDeliveryExtensionStartDate { get; set; }
  public long? AgreedDeliveryExtensionEndDate { get; set; }
  public string InvoiceLink { get; set; }
  public bool FastDelivery { get; set; }
  public string FastDeliveryType { get; set; }
  public long OriginShipmentDate { get; set; }
  public long LastModifiedDate { get; set; }
  public bool Commercial { get; set; }
  public bool DeliveredByService { get; set; }
  public bool Micro { get; set; }
  public bool GiftBoxRequested { get; set; }
  public string EtgbNo { get; set; }
  public long EtgbDate { get; set; }
}