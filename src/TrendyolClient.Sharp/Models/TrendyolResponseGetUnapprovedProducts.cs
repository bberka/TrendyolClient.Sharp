using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetUnapprovedProducts
{
    [JsonPropertyName("totalElements")]
    public long TotalElements { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("nextPageToken")]
    public string NextPageToken { get; set; }

    [JsonPropertyName("content")]
    public List<TrendyolUnapprovedProduct> Content { get; set; }
}
