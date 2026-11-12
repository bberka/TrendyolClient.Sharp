namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Paging parameters for cargo invoice items
/// </summary>
public class TrendyolFilterGetCargoInvoiceItems
{
    /// <summary>
    ///     Page number, starting from 0
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    ///     Page size (default: 500)
    /// </summary>
    public int? Size { get; set; }
}
