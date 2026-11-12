using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Item of an approved product delivery info bulk update.
/// </summary>
public class TrendyolUpdateDeliveryInfoItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("deliveryOptions")]
    public TrendyolProductDeliveryOption DeliveryOptions { get; set; }
}
