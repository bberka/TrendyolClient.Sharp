using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestUpdateProduct
{
  public List<TrendyolUpdateProduct> Items { get; set; }
}