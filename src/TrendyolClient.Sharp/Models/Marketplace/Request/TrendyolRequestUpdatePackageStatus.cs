using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestUpdatePackageStatus
{
  public List<PackageStatusLine> Lines { get; set; }
  public PackageStatusParams Params { get; set; }
  public string Status { get; set; }
}