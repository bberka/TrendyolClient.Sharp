namespace TrendyolClient.Sharp.Models.Marketplace.Response;

public class TrendyolResponseGetSuppliersAddresses
{
  public TrendyolSupplierAddress[] SupplierAddresses { get; set; }

  public TrendyolSupplierAddress DefaultShipmentAddress { get; set; }

  public TrendyolSupplierAddress DefaultInvoiceAddress { get; set; }

  public TrendyolDefaultReturningAddress GetSuppliersAddressesDefaultReturningAddress { get; set; }
}