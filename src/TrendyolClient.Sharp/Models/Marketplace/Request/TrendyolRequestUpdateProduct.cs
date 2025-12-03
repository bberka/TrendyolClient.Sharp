using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestUpdateProduct
{
  public List<TrendyolUpdateProduct> Items { get; set; }
}