using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseCreateClaim
{
    [JsonPropertyName("claimId")]
    public string ClaimId { get; set; }

    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("cargoTrackingNumber")]
    public string CargoTrackingNumber { get; set; }

    [JsonPropertyName("claimItemIds")]
    public List<string> ClaimItemIds { get; set; }
}
