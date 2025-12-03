using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestApproveClaimLineItems
{
  public List<string> ClaimLineItemIdList { get; set; }
  public Dictionary<string, object> Params { get; set; }
}