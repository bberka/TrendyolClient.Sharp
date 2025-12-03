using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Response;

public class TrendyolResponseGetCategoryAttributes
{
  public long Id { get; set; }


  public string Name { get; set; }


  public string DisplayName { get; set; }

  public List<TrendyolCategoryAttribute> CategoryAttributes { get; set; }
}