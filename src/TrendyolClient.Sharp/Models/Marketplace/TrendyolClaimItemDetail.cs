namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolClaimItemDetail
{
  public string Id { get; set; }
  public long OrderLineItemId { get; set; }
  public TrendyolClaimItemReason CustomerClaimItemReason { get; set; }
  public TrendyolClaimItemReason TrendyolClaimItemReason { get; set; }
  public TrendyolClaimItemStatus ClaimItemStatus { get; set; }
  public string Note { get; set; }
  public string CustomerNote { get; set; }
  public bool Resolved { get; set; }
}