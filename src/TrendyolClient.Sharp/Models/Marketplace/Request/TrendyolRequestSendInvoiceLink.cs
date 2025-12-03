namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestSendInvoiceLink
{
  public string InvoiceLink { get; set; }
  public long ShipmentPackageId { get; set; }
  public long InvoiceDateTime { get; set; }
  public string InvoiceNumber { get; set; }
}