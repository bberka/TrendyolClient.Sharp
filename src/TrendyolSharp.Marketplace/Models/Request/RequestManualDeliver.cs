﻿namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestManualDeliver
{
  public bool IsPhoneNumber { get; set; }
  public string TrackingInfo { get; set; }
  public Dictionary<string, string> Params { get; set; }
}
