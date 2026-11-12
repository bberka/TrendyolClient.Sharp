using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for the Global Partner offer content medias
/// </summary>
public class TrendyolGlobalFilterGetOfferContentMedias
{
    /// <summary>
    ///     Content IDs that group variants (required)
    /// </summary>
    public List<string> ContentIds { get; set; }
}
