﻿namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class Attribute
{
  public long AttributeId { get; set; }

  public long AttributeValueId { get; set; }

  public string? CustomAttributeValue { get; set; }
}