using System.Collections.Generic;
using System.Linq;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Thrown by <c>TrendyolResponseGetBatchRequest.EnsureSuccess()</c> when Trendyol accepted a product operation (HTTP 200) but
///     rejected some or all of its items afterwards.
/// </summary>
public class TrendyolBatchRequestFailedException(string batchRequestId, int failedItemCount, IReadOnlyList<TrendyolBatchRequestItem> failedItems)
    : TrendyolException($"Batch request {batchRequestId} has {failedItemCount} failed item(s): {Summarize(failedItems)}")
{
    public string BatchRequestId { get; } = batchRequestId;

    /// <summary>
    ///     Number of failed items, as reported by Trendyol.
    /// </summary>
    public int FailedItemCount { get; } = failedItemCount;

    /// <summary>
    ///     The failed items with their <see cref="TrendyolBatchRequestItem.FailureReasons" />. May hold fewer entries than
    ///     <see cref="FailedItemCount" /> when Trendyol only returns a page of the items.
    /// </summary>
    public IReadOnlyList<TrendyolBatchRequestItem> FailedItems { get; } = failedItems;

    private static string Summarize(IReadOnlyList<TrendyolBatchRequestItem> items)
    {
        var reasons = items.SelectMany(i => i.FailureReasons ?? [])
            .Distinct()
            .Take(3)
            .ToList();
        return reasons.Count == 0 ? "see FailedItems" : string.Join("; ", reasons);
    }
}
