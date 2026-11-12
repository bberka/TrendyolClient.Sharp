using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Exceptions;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetBatchRequest
{
    [JsonPropertyName("batchRequestId")]
    public string BatchRequestId { get; set; }

    /// <summary>
    ///     COMPLETED or IN_PROGRESS. Not returned for stock/price updates; check the item statuses instead.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("creationDate")]
    public long CreationDate { get; set; }

    [JsonPropertyName("lastModification")]
    public long LastModification { get; set; }

    /// <summary>
    ///     API or WEB.
    /// </summary>
    [JsonPropertyName("sourceType")]
    public string SourceType { get; set; }

    [JsonPropertyName("itemCount")]
    public int ItemCount { get; set; }

    [JsonPropertyName("failedItemCount")]
    public int FailedItemCount { get; set; }

    /// <summary>
    ///     ProductV2OnBoarding, ProductV2Update, ApprovedProductContentUpdate, ApprovedProductVariantUpdate, ProductDeliveryOptionUpdate, ProductInventoryUpdate, ProductArchiveUpdate, ProductDeletion, ProductUnlockUpdate.
    /// </summary>
    [JsonPropertyName("batchRequestType")]
    public string BatchRequestType { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolBatchRequestItem> Items { get; set; }

    /// <summary>
    ///     <c>true</c> when Trendyol has finished the batch: status COMPLETED, or, for operations that return no batch status (stock and
    ///     price updates), when no item is IN_PROGRESS any more.
    /// </summary>
    [JsonIgnore]
    public bool IsCompleted =>
        string.Equals(Status, "COMPLETED", StringComparison.OrdinalIgnoreCase)
        || (Status == null && Items is { Count: > 0 } && Items.All(i => !string.Equals(i.Status, "IN_PROGRESS", StringComparison.OrdinalIgnoreCase)));

    /// <summary>
    ///     The items Trendyol rejected, with their failure reasons.
    /// </summary>
    [JsonIgnore]
    public IReadOnlyList<TrendyolBatchRequestItem> FailedItems =>
        (Items ?? []).Where(i => string.Equals(i.Status, "FAILED", StringComparison.OrdinalIgnoreCase))
        .ToList();

    /// <summary>
    ///     <c>true</c> when Trendyol reports failed items.
    /// </summary>
    [JsonIgnore]
    public bool HasFailures => FailedItemCount > 0 || FailedItems.Count > 0;

    /// <summary>
    ///     Product operations answer HTTP 200 when they are queued; rejected items only show up here. Throws
    ///     <see cref="TrendyolBatchRequestFailedException" /> when any item failed, and returns this result otherwise.
    ///     A batch that is still running has no failures yet: check <see cref="IsCompleted" /> first.
    /// </summary>
    public TrendyolResponseGetBatchRequest EnsureSuccess()
    {
        if (HasFailures) throw new TrendyolBatchRequestFailedException(BatchRequestId, Math.Max(FailedItemCount, FailedItems.Count), FailedItems);
        return this;
    }
}
