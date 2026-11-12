using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetProductBase
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("approved")]
    public bool Approved { get; set; }

    [JsonPropertyName("approvedDate")]
    public long? ApprovedDate { get; set; }

    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    [JsonPropertyName("listingId")]
    public string ListingId { get; set; }

    [JsonPropertyName("contentId")]
    public long ContentId { get; set; }
}
