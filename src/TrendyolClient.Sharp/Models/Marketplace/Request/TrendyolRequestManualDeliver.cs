using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestManualDeliver
{
  public bool IsPhoneNumber { get; set; }
  public string TrackingInfo { get; set; }
  public Dictionary<string, string> Params { get; set; }
}