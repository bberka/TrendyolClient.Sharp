namespace TrendyolClient.Sharp.Models.Marketplace.Filter;

public sealed class FilterGetCategoryAttributes
{
  public string Name { get; set; }
  public string DisplayName { get; set; }
  public long? AttributeId { get; set; }
  public string AttributeName { get; set; }
  public bool? AllowCustom { get; set; }
  public bool? Required { get; set; }
  public bool? Slicer { get; set; }
  public bool? Varianter { get; set; }
  public long? AttributeValueId { get; set; }
  public string AttributeValueName { get; set; }
}