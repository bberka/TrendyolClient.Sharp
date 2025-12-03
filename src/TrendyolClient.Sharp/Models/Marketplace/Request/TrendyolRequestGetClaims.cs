namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestGetClaims
{
  public string ClaimIds { get; set; }
  public string ClaimItemStatus { get; set; }
  public long? EndDate { get; set; }
  public long? StartDate { get; set; }
  public string OrderNumber { get; set; }
  public long? Size { get; set; }
  public long? Page { get; set; }
}