using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetQuestions
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>WAITING_FOR_ANSWER, ANSWERED, REPORTED, REJECTED or UNANSWERED.</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>Timestamp (ms). Without dates the last week is returned; the maximum range is two weeks.</summary>
    [JsonPropertyName("startDate")]
    public long? StartDate { get; set; }

    /// <summary>Timestamp (ms).</summary>
    [JsonPropertyName("endDate")]
    public long? EndDate { get; set; }

    /// <summary>CreatedDate or LastModifiedDate.</summary>
    [JsonPropertyName("orderByField")]
    public string OrderByField { get; set; }

    /// <summary>ASC or DESC.</summary>
    [JsonPropertyName("orderByDirection")]
    public string OrderByDirection { get; set; }

    /// <summary>Zero-based page index.</summary>
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>Page size (default 20, max 50).</summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }
}
