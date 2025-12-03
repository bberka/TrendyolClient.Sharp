using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolCategoryAttribute
{
  public long CategoryId { get; set; }

  public TrendyolCategoryAttributeName AttributeName { get; set; }

  public bool Required { get; set; }

  public bool AllowCustom { get; set; }

  public bool Varianter { get; set; }

  public bool Slicer { get; set; }

  public List<TrendyolCategoryAttributeValue> AttributeValues { get; set; }
}