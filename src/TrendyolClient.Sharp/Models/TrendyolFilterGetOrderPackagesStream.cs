using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Query parameters of getShipmentPackagesStream.
/// </summary>
public class TrendyolFilterGetOrderPackagesStream
{
    /// <summary>
    ///     Comma separated package statuses.
    /// </summary>
    [JsonPropertyName("packageItemStatuses")]
    public string PackageItemStatuses { get; set; }

    /// <summary>
    ///     Milliseconds. Defaults to the last two weeks when omitted.
    /// </summary>
    [JsonPropertyName("lastModifiedStartDate")]
    public long? LastModifiedStartDate { get; set; }

    /// <summary>
    ///     Milliseconds. The range is limited to 14 days.
    /// </summary>
    [JsonPropertyName("lastModifiedEndDate")]
    public long? LastModifiedEndDate { get; set; }

    /// <summary>
    ///     Default 50, maximum 200.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }

    /// <summary>
    ///     Cursor from the previous response; omit on the first request. Filters must not change while paging with a cursor.
    /// </summary>
    [JsonPropertyName("nextCursor")]
    public string NextCursor { get; set; }
}
