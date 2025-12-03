using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestCreateProducts
{
  public List<CreateProduct> Items { get; set; }
}