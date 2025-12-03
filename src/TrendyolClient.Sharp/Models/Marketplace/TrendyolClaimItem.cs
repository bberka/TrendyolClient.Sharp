using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolClaimItem
{
  public TrendyolOrderLine OrderLine { get; set; }
  public List<TrendyolClaimItemDetail> ClaimItems { get; set; }
}