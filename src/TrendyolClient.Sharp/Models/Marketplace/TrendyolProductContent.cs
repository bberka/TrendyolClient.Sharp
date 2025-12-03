using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class TrendyolProductContent
{
  public string Id { get; set; }


  public bool Approved { get; set; }


  public bool Archived { get; set; }

  public long ProductCode { get; set; }


  public string BatchRequestId { get; set; }


  public long SupplierId { get; set; }


  public long CreateDateTime { get; set; }


  public long LastUpdateDate { get; set; }

  public string Gender { get; set; }


  public string Brand { get; set; }


  public string Barcode { get; set; }


  public string Title { get; set; }


  public string CategoryName { get; set; }


  public string ProductMainId { get; set; }


  public string Description { get; set; }

  public string StockUnitType { get; set; }


  public long Quantity { get; set; }


  public decimal ListPrice { get; set; }


  public decimal SalePrice { get; set; }


  public long VatRate { get; set; }


  public decimal DimensionalWeight { get; set; }


  public string StockCode { get; set; }


  public DeliveryOption DeliveryOption { get; set; }


  public List<Image> Images { get; set; }


  public List<TrendyolProductsAttribute> Attributes { get; set; }

  public string PlatformListingId { get; set; }


  public string StockId { get; set; }


  public bool HasActiveCampaign { get; set; }


  public bool Locked { get; set; }


  public long ProductContentId { get; set; }


  public long PimCategoryId { get; set; }


  public long BrandId { get; set; }


  public long Version { get; set; }


  public string Color { get; set; }


  public string Size { get; set; }


  public bool LockedByUnSuppliedReason { get; set; }


  public bool Onsale { get; set; }


  public string ProductUrl { get; set; }
}