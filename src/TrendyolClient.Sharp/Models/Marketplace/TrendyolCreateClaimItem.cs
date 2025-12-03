namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolCreateClaimItem
{
  public string Barcode { get; set; }
  public string CustomerNote { get; set; }
  public long Quantity { get; set; }
  public long ReasonId { get; set; }
}