namespace TrendyolClient.Sharp.Models.Marketplace.Filter;

public class TrendyolFilterGetCategoryTree
{
  public long? Id { get; set; }
  public long? ParentId { get; set; }
  public string Name { get; set; }
  public bool? SubCategories { get; set; }
}