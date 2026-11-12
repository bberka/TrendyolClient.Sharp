using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimAuditUserInfo
{
    [JsonPropertyName("executorId")]
    public string ExecutorId { get; set; }

    /// <summary>SellerIntegrationApi for API calls, Seller Center Orders BFF for the seller panel.</summary>
    [JsonPropertyName("executorApp")]
    public string ExecutorApp { get; set; }

    [JsonPropertyName("executorUser")]
    public long ExecutorUser { get; set; }
}
