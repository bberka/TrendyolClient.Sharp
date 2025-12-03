using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestUpdatePackageStatus
{
  public List<TrendyolPackageStatusLine> Lines { get; set; }
  public TrendyolPackageStatusParams Params { get; set; }
  public string Status { get; set; }
}