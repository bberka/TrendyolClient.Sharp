namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class OrderLine
{
  public long Id { get; set; }
  public string ProductName { get; set; }
  public string Barcode { get; set; }
  public string MerchantSku { get; set; }
  public string ProductColor { get; set; }
  public string ProductSize { get; set; }
  public double Price { get; set; }
  public long VatBaseAmount { get; set; }
  public long SalesCampaignId { get; set; }
  public string ProductCategory { get; set; }
}