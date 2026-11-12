using System.Text.Json;
using Refit;
using TrendyolClient.Sharp.Exceptions;
using TrendyolClient.Sharp.Models;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

/// <summary>Product writes answer 200 when queued; these helpers surface what Trendyol rejects afterwards.</summary>
public class BatchResultTests
{
    private static TrendyolResponseGetBatchRequest Parse(string json)
    {
        return JsonSerializer.Deserialize<TrendyolResponseGetBatchRequest>(json, TrendyolJson.CreateOptions());
    }

    private const string Completed = """
        { "batchRequestId": "b-1", "status": "COMPLETED", "itemCount": 2, "failedItemCount": 0,
          "items": [ { "requestItem": {"barcode":"A"}, "status": "SUCCESS", "failureReasons": [] },
                     { "requestItem": {"barcode":"B"}, "status": "SUCCESS", "failureReasons": [] } ] }
        """;

    private const string CompletedWithFailure = """
        { "batchRequestId": "b-2", "status": "COMPLETED", "itemCount": 2, "failedItemCount": 1,
          "items": [ { "requestItem": {"barcode":"A"}, "status": "SUCCESS", "failureReasons": [] },
                     { "requestItem": {"barcode":"B"}, "status": "FAILED", "failureReasons": ["Barcode already exists", "Brand not found"] } ] }
        """;

    [Fact]
    public void A_clean_batch_is_completed_without_failures()
    {
        var result = Parse(Completed);

        Assert.True(result.IsCompleted);
        Assert.False(result.HasFailures);
        Assert.Empty(result.FailedItems);
        Assert.Same(result, result.EnsureSuccess());
    }

    [Fact]
    public void Failed_items_are_exposed_with_their_reasons()
    {
        var result = Parse(CompletedWithFailure);

        Assert.True(result.IsCompleted);
        Assert.True(result.HasFailures);
        var failed = Assert.Single(result.FailedItems);
        Assert.Equal(["Barcode already exists", "Brand not found"], failed.FailureReasons);
    }

    [Fact]
    public void EnsureSuccess_throws_with_the_batch_id_and_the_reasons()
    {
        var ex = Assert.Throws<TrendyolBatchRequestFailedException>(() => Parse(CompletedWithFailure).EnsureSuccess());

        Assert.Equal("b-2", ex.BatchRequestId);
        Assert.Equal(1, ex.FailedItemCount);
        Assert.Contains("Barcode already exists", ex.Message);
        Assert.Single(ex.FailedItems);
    }

    [Fact]
    public void A_failed_count_without_item_details_still_counts_as_a_failure()
    {
        var result = Parse("""{ "batchRequestId": "b-3", "status": "COMPLETED", "itemCount": 5, "failedItemCount": 5, "items": [] }""");

        Assert.True(result.HasFailures);
        var ex = Assert.Throws<TrendyolBatchRequestFailedException>(() => result.EnsureSuccess());
        Assert.Equal(5, ex.FailedItemCount);
        Assert.Contains("see FailedItems", ex.Message);
    }

    [Fact]
    public void A_running_batch_is_not_completed_and_has_no_failures_yet()
    {
        var result = Parse("""{ "batchRequestId": "b-4", "status": "IN_PROGRESS", "itemCount": 2, "failedItemCount": 0, "items": [] }""");

        Assert.False(result.IsCompleted);
        Assert.False(result.HasFailures);
    }

    [Theory]
    [InlineData("SUCCESS", true)]
    [InlineData("IN_PROGRESS", false)]
    public void Stock_and_price_batches_have_no_status_so_the_item_statuses_decide(string itemStatus, bool completed)
    {
        // Documented: "status" is not returned for stock/price updates; check the item statuses instead.
        var result = Parse($$"""{ "batchRequestId": "b-5", "items": [ { "requestItem": {"barcode":"A"}, "status": "{{itemStatus}}" } ] }""");

        Assert.Equal(completed, result.IsCompleted);
    }

    [Fact]
    public void The_helpers_are_not_part_of_the_JSON()
    {
        var json = JsonSerializer.Serialize(Parse(CompletedWithFailure), TrendyolJson.CreateOptions());

        Assert.DoesNotContain("isCompleted", json);
        Assert.DoesNotContain("hasFailures", json);
        Assert.DoesNotContain("failedItems", json);
    }

    [Fact]
    public void Paged_responses_share_one_envelope()
    {
        Assert.IsAssignableFrom<TrendyolPagedResponse<TrendyolClaim>>(new TrendyolResponseGetClaims());
        Assert.IsAssignableFrom<TrendyolTokenPagedResponse<TrendyolUnapprovedProduct>>(new TrendyolResponseGetUnapprovedProducts());
        Assert.IsAssignableFrom<TrendyolFinancePagedResponse<TrendyolPaymentOrder>>(new TrendyolResponseGetPaymentOrders());
    }
}
