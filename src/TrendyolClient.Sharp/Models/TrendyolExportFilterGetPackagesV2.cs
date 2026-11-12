namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for listing Export Center packages (V2)
/// </summary>
public class TrendyolExportFilterGetPackagesV2
{
    /// <summary>
    ///     Tracking number of the items awaiting supply
    /// </summary>
    public string TrackingNumber { get; set; }

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
    public long? ApprovedStartDate { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    public long? ApprovedEndDate { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    public long? CreationStartDate { get; set; }

    /// <summary>
    ///     UTC unix timestamp, milliseconds
    /// </summary>
    public long? CreationEndDate { get; set; }

    /// <summary>
    ///     None
    /// </summary>
    public string BoutiqueId { get; set; }
}
