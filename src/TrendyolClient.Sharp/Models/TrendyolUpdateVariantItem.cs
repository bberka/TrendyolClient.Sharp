using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Item of an approved product variant bulk update. Partial update is supported: send barcode plus the fields to change.
/// </summary>
public class TrendyolUpdateVariantItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     CORE and/or LUXE. Send only to open or close a sales channel; an empty list is rejected.
    /// </summary>
    [JsonPropertyName("channels")]
    public List<string> Channels { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("vatRate")]
    public int? VatRate { get; set; }

    [JsonPropertyName("shipmentAddressId")]
    public long? ShipmentAddressId { get; set; }

    [JsonPropertyName("returningAddressId")]
    public long? ReturningAddressId { get; set; }

    [JsonPropertyName("dimensionalWeight")]
    public decimal? DimensionalWeight { get; set; }

    [JsonPropertyName("lotNumber")]
    public string LotNumber { get; set; }

    [JsonPropertyName("cargoProviders")]
    public List<string> CargoProviders { get; set; }

    /// <summary>
    ///     ENABLED, DISABLED or null.
    /// </summary>
    [JsonPropertyName("locationBasedDelivery")]
    public string LocationBasedDelivery { get; set; }
}
