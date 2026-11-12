using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalResponseGetOffers
{
    [JsonPropertyName("offers")]
    public List<TrendyolGlobalOffer> Offers { get; set; }

    /// <summary>
    ///     Next page key
    /// </summary>
    [JsonPropertyName("pageKey")]
    public string PageKey { get; set; }

    [JsonPropertyName("totalCount")]
    public long TotalCount { get; set; }
}
