using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportPackage
{
    /// <summary>
    ///     Tracking number created for the items awaiting supply
    /// </summary>
    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("trackingNumber")]
    public string TrackingNumber { get; set; }

    [JsonPropertyName("packageId")]
    public string PackageId { get; set; }

    [JsonPropertyName("totalQuantity")]
    public int TotalQuantity { get; set; }

    /// <summary>
    ///     Sum of buying prices of the items, VAT included
    /// </summary>
    [JsonPropertyName("totalBuyingPrice")]
    public decimal TotalBuyingPrice { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    /// <summary>
    ///     new, pending, completed or cancelled
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("boutiqueId")]
    public string BoutiqueId { get; set; }

    [JsonPropertyName("platformId")]
    public int PlatformId { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    [JsonPropertyName("creationDate")]
    public long CreationDate { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    [JsonPropertyName("approvedDate")]
    public long ApprovedDate { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public long LastModifiedDate { get; set; }

    [JsonPropertyName("cargos")]
    public List<TrendyolExportCargoInfo> Cargos { get; set; }
}
