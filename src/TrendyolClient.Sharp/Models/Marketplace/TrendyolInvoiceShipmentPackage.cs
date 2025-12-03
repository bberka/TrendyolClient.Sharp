namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolInvoiceShipmentPackage
{
  public string ShipmentPackageType { get; set; }
  public long ParcelUniqueId { get; set; }
  public string OrderNumber { get; set; }
  public decimal Amount { get; set; }
  public long Desi { get; set; }
}