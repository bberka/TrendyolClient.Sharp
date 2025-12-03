using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolProductDeliveryOption
{
    [JsonPropertyName("deliveryDuration")]
    public int DeliveryDuration { get; set; }

    [JsonPropertyName("fastDeliveryType")]
    public string FastDeliveryType { get; set; }
}