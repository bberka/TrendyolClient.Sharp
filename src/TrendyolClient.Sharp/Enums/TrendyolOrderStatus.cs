using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Enums;

/// <summary>
/// Order statuses that can trigger webhook notifications
/// Based on Trendyol order lifecycle
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TrendyolOrderStatus
{
    /// <summary>
    /// Order has been created and is awaiting seller confirmation
    /// </summary>
    [EnumMember(Value = "CREATED")]
    Created,

    /// <summary>
    /// Order is being prepared/picked from warehouse
    /// </summary>
    [EnumMember(Value = "PICKING")]
    Picking,

    /// <summary>
    /// Order items have been picked and ready for shipment
    /// </summary>
    [EnumMember(Value = "PICKED")]
    Picked,

    /// <summary>
    /// Order has been shipped to customer
    /// </summary>
    [EnumMember(Value = "SHIPPED")]
    Shipped,

    /// <summary>
    /// Order has been delivered to customer
    /// </summary>
    [EnumMember(Value = "DELIVERED")]
    Delivered,

    /// <summary>
    /// Order has been cancelled (by customer or seller)
    /// </summary>
    [EnumMember(Value = "CANCELLED")]
    Cancelled,

    /// <summary>
    /// Order has been returned by customer
    /// </summary>
    [EnumMember(Value = "RETURNED")]
    Returned,

    /// <summary>
    /// Order is in return process
    /// </summary>
    [EnumMember(Value = "RETURNING")]
    Returning,

    /// <summary>
    /// Order is awaiting action/confirmation
    /// </summary>
    [EnumMember(Value = "AWAITING")]
    Awaiting
}