using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetBuyboxInformation
{
    [JsonPropertyName("buyboxInfo")]
    public List<TrendyolBuyboxInfo> BuyboxInfo { get; set; }
}
