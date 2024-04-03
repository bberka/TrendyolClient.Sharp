﻿namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class ShipmentAddress
{
  public long Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Company { get; set; }
  public string Address1 { get; set; }
  public string Address2 { get; set; }
  public string City { get; set; }
  public string District { get; set; }
  public int DistrictId { get; set; }
  public int CityCode { get; set; }
  public string PostalCode { get; set; }
  public string CountryCode { get; set; }
  public int NeighborhoodId { get; set; }
  public string Neighborhood { get; set; }
  public string Phone { get; set; }
  public string FullName { get; set; }
  public string FullAddress { get; set; }

}