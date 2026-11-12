namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for listing payment orders
/// </summary>
public class TrendyolFilterGetPaymentOrders
{
    /// <summary>
    ///     Page number, starting from 0
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    ///     Page size (default and maximum: 10)
    /// </summary>
    public int? Size { get; set; }
}
