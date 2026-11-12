using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportResponseGetPackagesV3
{
    /// <summary>
    ///     Current page number
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }

    /// <summary>
    ///     Page size
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("totalItemCount")]
    public long TotalItemCount { get; set; }

    [JsonPropertyName("totalPageCount")]
    public long TotalPageCount { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolExportPackageItemV3> Items { get; set; }
}
