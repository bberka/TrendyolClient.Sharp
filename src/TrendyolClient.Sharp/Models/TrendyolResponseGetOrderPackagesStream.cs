using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetOrderPackagesStream
{
    [JsonPropertyName("content")]
    public List<TrendyolOrderPackage> Content { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    /// <summary>
    ///     Opaque value to pass in the next request; empty when hasMore is false.
    /// </summary>
    [JsonPropertyName("nextCursor")]
    public string NextCursor { get; set; }
}
