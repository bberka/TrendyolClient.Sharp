using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class ClaimItem
{
  public OrderLine OrderLine { get; set; }
  public List<ClaimItemDetail> ClaimItems { get; set; }
}