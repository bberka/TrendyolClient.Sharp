﻿namespace TrendyolClient.Sharp.Models.Marketplace.Request
{
  public class RequestCreateClaimIssue
  {
    public string ClaimId { get; set; }
    public long ClaimIssueReasonId { get; set; }
    public string ClaimItemIdList { get; set; }
    public string Description { get; set; }
  }
}