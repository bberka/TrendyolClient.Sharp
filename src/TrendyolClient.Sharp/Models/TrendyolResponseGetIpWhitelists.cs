using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetIpWhitelists
{
    [JsonPropertyName("sellerId")]
    public string SellerId { get; set; }

    /// <summary>
    ///     Whitelisted IPs (max 7, may be empty)
    /// </summary>
    [JsonPropertyName("ips")]
    public List<TrendyolIpWhitelistEntry> Ips { get; set; }
}
