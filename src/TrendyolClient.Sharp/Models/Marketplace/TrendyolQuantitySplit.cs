using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolQuantitySplit
{
  public long OrderLineId { get; set; }
  public List<long> Quantities { get; set; }
}