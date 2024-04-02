﻿namespace TrendyolSharp.Marketplace.Models.Filter;

public sealed class FilterProducts
{
  public bool? Approved { get; set; }
  public string? Barcode { get; set; }
  public long? StartDate { get; set; }
  public long? EndDate { get; set; }
  public int? Page { get; set; }
  public string? DateQueryType { get; set; }
  public int? Size { get; set; }
  public long? SupplierId { get; set; }
  public string? StockCode { get; set; }
  public bool? Archived { get; set; }
  public string? ProductMainId { get; set; }
  public bool? OnSale { get; set; }
  public bool? Rejected { get; set; }
  public bool? Blacklisted { get; set; }
  public string[]? BrandIds { get; set; }
}