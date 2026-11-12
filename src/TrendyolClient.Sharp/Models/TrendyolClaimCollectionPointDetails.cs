using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimCollectionPointDetails
{
    [JsonPropertyName("collectionPointId")]
    public string CollectionPointId { get; set; }
}
