using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Query parameters of getShipmentPackages (V2).
/// </summary>
public class TrendyolFilterGetOrderPackages
{
    /// <summary>
    ///     Package status; several can be comma separated (e.g. "Cancelled,UnSupplied"). "UnPacked" returns split packages.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>
    ///     Maximum 50 package ids.
    /// </summary>
    [JsonPropertyName("shipmentPackageIds")]
    public List<long> ShipmentPackageIds { get; set; }

    [JsonPropertyName("storeFrontCode")]
    public string StoreFrontCode { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Milliseconds. With a date range the maximum span is two weeks; without one the last week is returned.
    /// </summary>
    [JsonPropertyName("startDate")]
    public long? StartDate { get; set; }

    /// <summary>
    ///     Milliseconds.
    /// </summary>
    [JsonPropertyName("endDate")]
    public long? EndDate { get; set; }

    /// <summary>
    ///     "PackageLastModifiedDate" or "CreatedDate".
    /// </summary>
    [JsonPropertyName("orderByField")]
    public string OrderByField { get; set; }

    /// <summary>
    ///     "ASC" or "DESC".
    /// </summary>
    [JsonPropertyName("orderByDirection")]
    public string OrderByDirection { get; set; }

    /// <summary>
    ///     Zero based. Only pages 0 to 49 (with size 200) are reachable because of the 10,000 package window.
    /// </summary>
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>
    ///     Maximum 200.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }
}
