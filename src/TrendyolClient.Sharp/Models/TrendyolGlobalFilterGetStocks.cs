using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for the Global Partner stock query
/// </summary>
public class TrendyolGlobalFilterGetStocks
{
    /// <summary>
    ///     Variant IDs (required, max 20)
    /// </summary>
    public List<string> VariantIds { get; set; }
}
