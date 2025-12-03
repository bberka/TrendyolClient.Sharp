using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestUpdatePriceAndInventory
{
  public List<TrendyolProductPriceAndInventoryInfo> Items { get; set; }
}