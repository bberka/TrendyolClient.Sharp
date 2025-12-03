namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class TrendyolProductPriceAndInventoryInfo
{
  public string Barcode { get; set; }


  public long Quantity { get; set; }


  public decimal SalePrice { get; set; }


  public decimal ListPrice { get; set; }
}