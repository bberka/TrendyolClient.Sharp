using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetAddresses
{
    [JsonPropertyName("addresses")]
    public List<TrendyolAddress> Addresses { get; set; }
}