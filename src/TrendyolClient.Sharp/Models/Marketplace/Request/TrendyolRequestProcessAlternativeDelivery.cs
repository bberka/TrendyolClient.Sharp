using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestProcessAlternativeDelivery
{
  public bool IsPhoneNumber { get; set; }
  public string TrackingInfo { get; set; }
  public Dictionary<string, object> Params { get; set; }
  public long? BoxQuantity { get; set; }
  public decimal? Deci { get; set; }
}