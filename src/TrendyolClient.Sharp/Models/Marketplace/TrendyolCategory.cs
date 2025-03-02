﻿using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace
{
  public sealed class Category
  {
    public long Id { get; set; }


    public string Name { get; set; }

    public long ParentId { get; set; }

    public List<TrendyolSubCategory> SubCategories { get; set; }
  }
}