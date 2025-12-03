using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestCreateProducts
{
  public List<TrendyolCreateProduct> Items { get; set; }
}