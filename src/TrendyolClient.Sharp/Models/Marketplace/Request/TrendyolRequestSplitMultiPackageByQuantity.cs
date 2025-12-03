using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestSplitMultiPackageByQuantity
{
  public List<PackageDetailGroup> SplitPackages { get; set; }
}