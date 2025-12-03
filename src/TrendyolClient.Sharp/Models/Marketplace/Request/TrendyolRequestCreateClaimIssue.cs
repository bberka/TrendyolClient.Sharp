namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestCreateClaimIssue
{
  public long ClaimIssueReasonId { get; set; }
  public string ClaimItemIdList { get; set; }
  public string Description { get; set; }
}