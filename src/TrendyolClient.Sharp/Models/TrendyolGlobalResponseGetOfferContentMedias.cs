using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalResponseGetOfferContentMedias
{
    [JsonPropertyName("content_medias")]
    public List<TrendyolGlobalContentMedia> ContentMedias { get; set; }
}
