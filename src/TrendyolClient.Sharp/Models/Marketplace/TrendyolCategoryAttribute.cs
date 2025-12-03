using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class CategoryAttribute
{
  public long CategoryId { get; set; }

  public CategoryAttributeName AttributeName { get; set; }

  public bool Required { get; set; }

  public bool AllowCustom { get; set; }

  public bool Varianter { get; set; }

  public bool Slicer { get; set; }

  public List<CategoryAttributeValue> AttributeValues { get; set; }
}