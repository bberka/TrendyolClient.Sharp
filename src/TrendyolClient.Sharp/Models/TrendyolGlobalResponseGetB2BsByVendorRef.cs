using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalResponseGetB2BsByVendorRef
{
    [JsonPropertyName("vendorRef")]
    public string VendorRef { get; set; }

    [JsonPropertyName("b2bSummaries")]
    public List<TrendyolGlobalB2BSummary> B2bSummaries { get; set; }
}
