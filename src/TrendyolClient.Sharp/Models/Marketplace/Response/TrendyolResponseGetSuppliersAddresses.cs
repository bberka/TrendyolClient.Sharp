namespace TrendyolClient.Sharp.Models.Marketplace.Response;

public sealed class ResponseGetSuppliersAddresses
{
  public TrendyolSupplierAddress[] SupplierAddresses { get; set; }

  public TrendyolSupplierAddress DefaultShipmentAddress { get; set; }

  public TrendyolSupplierAddress DefaultInvoiceAddress { get; set; }

  public DefaultReturningAddress GetSuppliersAddressesDefaultReturningAddress { get; set; }
}