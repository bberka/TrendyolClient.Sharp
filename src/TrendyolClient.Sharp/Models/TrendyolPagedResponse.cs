using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     The page envelope most Trendyol list endpoints answer with: the items of one page plus where it sits in the result.
/// </summary>
/// <typeparam name="TItem">The element type of <see cref="Content" />.</typeparam>
public class TrendyolPagedResponse<TItem>
{
    [JsonPropertyName("content")]
    public List<TItem> Content { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("totalElements")]
    public long TotalElements { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}

/// <summary>
///     A <see cref="TrendyolPagedResponse{TItem}" /> that also carries a cursor for reading past the 10,000 item page window.
/// </summary>
public class TrendyolTokenPagedResponse<TItem> : TrendyolPagedResponse<TItem>
{
    /// <summary>
    ///     Pass as <c>nextPageToken</c> in the next request to continue. Absent on the last page.
    /// </summary>
    [JsonPropertyName("nextPageToken")]
    public string NextPageToken { get; set; }
}

/// <summary>
///     The page envelope of the finance endpoints, where Trendyol sends every number as a 64-bit integer.
/// </summary>
public class TrendyolFinancePagedResponse<TItem>
{
    [JsonPropertyName("content")]
    public List<TItem> Content { get; set; }

    [JsonPropertyName("page")]
    public long Page { get; set; }

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("totalElements")]
    public long TotalElements { get; set; }

    [JsonPropertyName("totalPages")]
    public long TotalPages { get; set; }
}
