using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Payment order (hakedis) record
/// </summary>
public class TrendyolPaymentOrder
{
    /// <summary>
    ///     Payment order ID; use it as paymentOrderId in settlements and otherfinancials
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Payment order status
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    ///     Payment amount
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    ///     Date the payment order was created (unix timestamp, milliseconds)
    /// </summary>
    [JsonPropertyName("payoutDate")]
    public long? PayoutDate { get; set; }

    [JsonPropertyName("paymentType")]
    public string PaymentType { get; set; }

    [JsonPropertyName("sellerId")]
    public long SellerId { get; set; }

    /// <summary>
    ///     Payment region ID
    /// </summary>
    [JsonPropertyName("regionId")]
    public long? RegionId { get; set; }

    /// <summary>
    ///     Payment region name
    /// </summary>
    [JsonPropertyName("regionName")]
    public string RegionName { get; set; }

    /// <summary>
    ///     Payment region code
    /// </summary>
    [JsonPropertyName("regionCode")]
    public string RegionCode { get; set; }
}
