using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolBatchRequestResultItem
{
  public TrendyolBatchRequestResultRequestItem RequestItem { get; set; }


  public string Status { get; set; }


  public List<object> FailureReasons { get; set; }
}