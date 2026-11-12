using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlClaimAuditUserInfo
{
    [JsonPropertyName("executorId")]
    public string ExecutorId { get; set; }

    /// <summary>SellerIntegrationApi for integration calls, Seller Center Orders BFF for the seller panel.</summary>
    [JsonPropertyName("executorApp")]
    public string ExecutorApp { get; set; }

    /// <summary>User email per the OpenAPI spec; the docs example shows a number, so both are read as text.</summary>
    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("executorUser")]
    public string ExecutorUser { get; set; }
}
