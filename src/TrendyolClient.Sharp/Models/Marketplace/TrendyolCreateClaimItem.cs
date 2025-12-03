namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class CreateClaimItem
{
  public string Barcode { get; set; }
  public string CustomerNote { get; set; }
  public long Quantity { get; set; }
  public long ReasonId { get; set; }
}