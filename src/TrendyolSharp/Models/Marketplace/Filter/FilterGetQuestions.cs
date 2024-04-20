﻿namespace TrendyolSharp.Models.Marketplace.Filter
{
  public sealed class FilterGetQuestions
  {
    public long? Barcode { get; set; }
    public long? Page { get; set; }
    public long? Size { get; set; }
    public long? SupplierId { get; set; }
    public long? StartDate { get; set; }
    public long? EndDate { get; set; }
    public string Status { get; set; }
    public string OrderByField { get; set; }
    public string OrderByDirection { get; set; }
  }
}