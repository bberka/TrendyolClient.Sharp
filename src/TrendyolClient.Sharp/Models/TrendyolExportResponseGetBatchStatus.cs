using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportResponseGetBatchStatus
{
    [JsonPropertyName("batchId")]
    public string BatchId { get; set; }

    /// <summary>
    ///     ProductCreate, StockUpdate, PriceUpdate...
    /// </summary>
    [JsonPropertyName("batchType")]
    public string BatchType { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolExportBatchItem> Items { get; set; }

    /// <summary>
    ///     COMPLETED, IN_PROGRESS or FAILED
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    ///     Unix timestamp, milliseconds
    /// </summary>
    [JsonPropertyName("creationDate")]
    public long CreationDate { get; set; }

    /// <summary>
    ///     Unix timestamp, milliseconds
    /// </summary>
    [JsonPropertyName("lastModification")]
    public long LastModification { get; set; }

    [JsonPropertyName("itemCount")]
    public int ItemCount { get; set; }
}
