using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class BatchRequestResultProduct
{
  public string Brand { get; set; }


  public string Barcode { get; set; }


  public string Title { get; set; }


  public string Description { get; set; }


  public string CategoryName { get; set; }


  public decimal ListPrice { get; set; }


  public decimal SalePrice { get; set; }


  public string CurrencyType { get; set; }


  public long VatRate { get; set; }


  public string CargoCompany { get; set; }


  public long Quantity { get; set; }


  public string StockCode { get; set; }


  public List<Image> Images { get; set; }


  public string ProductMainId { get; set; }


  public string Gender { get; set; }


  public decimal DimensionalWeight { get; set; }

  public List<object> Attributes { get; set; }

  public List<TrendyolVariantAttribute> VariantAttributes { get; set; }
}