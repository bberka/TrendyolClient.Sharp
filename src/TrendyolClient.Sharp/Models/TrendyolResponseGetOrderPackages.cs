using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetOrderPackages
{
    [JsonPropertyName("content")]
    public List<TrendyolOrderPackage> Content { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    ///     Total matching packages. Only the first 10,000 are reachable; use the stream endpoint when it is larger.
    /// </summary>
    [JsonPropertyName("totalElements")]
    public long TotalElements { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}
