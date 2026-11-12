namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for listing Export Center package items (V3)
/// </summary>
public class TrendyolExportFilterGetPackagesV3
{
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

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    public long? CreationStartDate { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    public long? CreationEndDate { get; set; }
}
