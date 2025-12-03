using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestDeleteProducts
{
  public List<TrendyolProductBarcode> Items { get; set; }
}