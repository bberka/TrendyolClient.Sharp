using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for listing Export Center products
/// </summary>
public class TrendyolExportFilterGetProducts
{
    /// <summary>
    ///     Barcodes to look up
    /// </summary>
    public List<string> Barcodes { get; set; }

    /// <summary>
    ///     Empty on the first request; afterwards the x-paging-key response header of the previous page
    /// </summary>
    public string PageKey { get; set; }

    /// <summary>
    ///     Page size (default 20, max 100)
    /// </summary>
    public int? Size { get; set; }
}
