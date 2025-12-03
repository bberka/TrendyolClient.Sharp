using Refit;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class RequestCreateClaimIssue
{
  public long ClaimIssueReasonId { get; set; }
  public string ClaimItemIdList { get; set; }
  public string Description { get; set; }
}