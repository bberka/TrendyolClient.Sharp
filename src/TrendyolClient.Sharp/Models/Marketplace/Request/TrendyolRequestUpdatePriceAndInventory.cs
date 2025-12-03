using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestUpdatePriceAndInventory
{
  public List<TrendyolProductPriceAndInventoryInfo> Items { get; set; }
}