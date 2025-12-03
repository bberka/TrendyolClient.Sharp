using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolLine
{
  public long Quantity { get; set; }
  public long SalesCampaignId { get; set; }
  public string ProductSize { get; set; }
  public string MerchantSku { get; set; }
  public string ProductName { get; set; }
  public long ProductCode { get; set; }
  public string ProductOrigin { get; set; }
  public long MerchantId { get; set; }
  public decimal Amount { get; set; }
  public decimal Discount { get; set; }
  public decimal TyDiscount { get; set; }
  public List<TrendyolDiscountDetail> DiscountDetails { get; set; }
  public List<TrendyolFastDeliveryOption> FastDeliveryOptions { get; set; }
  public string CurrencyCode { get; set; }
  public string ProductColor { get; set; }
  public long Id { get; set; }
  public string Sku { get; set; }
  public decimal VatBaseAmount { get; set; }
  public string Barcode { get; set; }
  public string OrderLineItemStatusName { get; set; }
  public decimal Price { get; set; }
}