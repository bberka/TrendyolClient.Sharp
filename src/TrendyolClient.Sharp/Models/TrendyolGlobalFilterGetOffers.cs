using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for listing Global Partner offers
/// </summary>
public class TrendyolGlobalFilterGetOffers
{
    /// <summary>
    ///     Model code that groups variants
    /// </summary>
    public string ModelNo { get; set; }

    /// <summary>
    ///     Variant IDs (max 100)
    /// </summary>
    public List<string> VariantIds { get; set; }

    /// <summary>
    ///     Product barcodes
    /// </summary>
    public List<string> Barcodes { get; set; }

    /// <summary>
    ///     Last modification start, RFC3339 (e.g. 2022-08-22T12:00:29Z)
    /// </summary>
    public string LastModifiedDateFrom { get; set; }

    /// <summary>
    ///     Last modification end, RFC3339
    /// </summary>
    public string LastModifiedDateTo { get; set; }

    /// <summary>
    ///     Page size (default 20, max 100)
    /// </summary>
    public int? Size { get; set; }

    /// <summary>
    ///     Next page key: the pageKey of the previous response, empty on the first call
    /// </summary>
    public string PageKey { get; set; }

    /// <summary>
    ///     Search by tags
    /// </summary>
    public List<string> Tags { get; set; }

    /// <summary>
    ///     Language code: EN, TR, PL, AR, DE, FR or NL
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    ///     Only active or inactive offers
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    ///     Brand ID (e.g. 40 = TrendyolMilla)
    /// </summary>
    public long? BrandId { get; set; }
}
