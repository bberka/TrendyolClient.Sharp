using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Enums;

/// <summary>
///     Package statuses a webhook can subscribe to. Serialized in upper snake case ("AT_COLLECTION_POINT").
///     https://developers.trendyol.com/docs/webhook-model
/// </summary>
[JsonConverter(typeof(TrendyolUpperSnakeCaseEnumConverter))]
public enum TrendyolOrderStatus
{
    Created,
    Picking,
    Invoiced,
    Shipped,
    Cancelled,
    Delivered,
    Undelivered,
    Returned,
    Unsupplied,
    Awaiting,
    Unpacked,
    AtCollectionPoint,
    Verified
}
