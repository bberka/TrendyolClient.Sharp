namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for the items of an Export Center package (V2)
/// </summary>
public class TrendyolExportFilterGetPackageItemsV2
{
    /// <summary>
    ///     Package ID (required)
    /// </summary>
    public string PackageId { get; set; }

    /// <summary>
    ///     new, pending, completed or cancelled
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    ///     Page number, starting from 1
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    ///     Page size (default 20, max 100)
    /// </summary>
    public int? Size { get; set; }
}
