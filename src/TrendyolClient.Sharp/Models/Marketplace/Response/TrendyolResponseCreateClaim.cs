using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Response;

public sealed class ResponseCreateClaim
{
  public string ClaimId { get; set; }
  public long CargoTrackingNumber { get; set; }
  public List<string> ClaimItemIds { get; set; }
}