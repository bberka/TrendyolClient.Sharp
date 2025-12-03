namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class TrendyolSupplierAddress
{
  public long Id { get; set; }


  public string AddressType { get; set; }


  public string Country { get; set; }


  public string City { get; set; }

  public long CityCode { get; set; }


  public string District { get; set; }

  public long DistrictId { get; set; }


  public string PostCode { get; set; }


  public string Address { get; set; }

  public bool ReturningAddress { get; set; }


  public string FullAddress { get; set; }

  public bool ShipmentAddress { get; set; }

  public bool InvoiceAddress { get; set; }

  public bool Default { get; set; }
}