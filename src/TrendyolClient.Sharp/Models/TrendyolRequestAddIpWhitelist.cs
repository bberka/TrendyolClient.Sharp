using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestAddIpWhitelist
{
    /// <summary>
    ///     IPv4 address to whitelist (IPv6 is not accepted)
    /// </summary>
    [JsonPropertyName("ip")]
    public string Ip { get; set; }
}
