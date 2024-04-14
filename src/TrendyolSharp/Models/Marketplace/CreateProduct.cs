﻿using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace
{
  public sealed class CreateProduct
  {
    public string Barcode { get; set; }

    public string Title { get; set; }


    public string ProductMainId { get; set; }


    public long BrandId { get; set; }


    public long CategoryId { get; set; }


    public long Quantity { get; set; }


    public string StockCode { get; set; }


    public decimal DimensionalWeight { get; set; }


    public string Description { get; set; }


    public string CurrencyType { get; set; }


    public decimal ListPrice { get; set; }


    public decimal SalePrice { get; set; }


    public long VatRate { get; set; }


    public long CargoCompanyId { get; set; }

    public long? ShipmentAddressId { get; set; }

    public long? ReturningAddressId { get; set; }

    public DeliveryOption DeliveryOption { get; set; }


    public List<Image> Images { get; set; }


    public List<Attribute> Attributes { get; set; }
  }
}