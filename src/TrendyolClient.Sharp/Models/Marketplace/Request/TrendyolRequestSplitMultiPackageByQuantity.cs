using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestSplitMultiPackageByQuantity
{
  public List<TrendyolPackageDetailGroup> SplitPackages { get; set; }
}