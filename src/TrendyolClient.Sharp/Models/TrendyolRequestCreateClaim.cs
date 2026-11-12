using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestCreateClaim
{
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    [JsonPropertyName("customerId")]
    public long CustomerId { get; set; }

    [JsonPropertyName("claimItems")]
    public List<TrendyolRequestCreateClaimItem> ClaimItems { get; set; }

    [JsonPropertyName("excludeListing")]
    public bool ExcludeListing { get; set; }

    [JsonPropertyName("forcePackageCreation")]
    public bool ForcePackageCreation { get; set; }

    /// <summary>Id of your cargo company. Optional.</summary>
    [JsonPropertyName("shipmentCompanyId")]
    public int? ShipmentCompanyId { get; set; }
}
