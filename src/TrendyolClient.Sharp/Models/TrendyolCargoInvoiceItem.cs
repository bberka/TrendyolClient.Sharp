using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
/// Represents a cargo invoice item with shipment package details
/// </summary>
public class TrendyolCargoInvoiceItem
{
    /// <summary>
    /// Type of shipment package (e.g., "Gönderi Kargo Bedeli" for shipment cargo cost, 
    /// "İade Kargo Bedeli" for return cargo cost)
    /// </summary>
    [JsonPropertyName("shipmentPackageType")]
    public string ShipmentPackageType { get; set; } = string.Empty;

    /// <summary>
    /// Unique identifier for the parcel
    /// </summary>
    [JsonPropertyName("parcelUniqueId")]
    public long ParcelUniqueId { get; set; }

    /// <summary>
    /// Order number associated with the cargo
    /// </summary>
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// Amount charged for the cargo
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Desi (volumetric weight) of the package
    /// </summary>
    [JsonPropertyName("desi")]
    public int Desi { get; set; }
}