using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolProductDeliveryOption
{
    /// <summary>
    ///     Shipping duration in days. Since August 2026: 0 = ships today, 1 = ships by tomorrow at the latest.
    /// </summary>
    [JsonPropertyName("deliveryDuration")]
    public int? DeliveryDuration { get; set; }

    /// <summary>
    ///     Obsolete since August 2026 (SAME_DAY_SHIPPING or FAST_DELIVERY). Trendyol ignores it; use deliveryDuration.
    /// </summary>
    [JsonPropertyName("fastDeliveryType")]
    public string FastDeliveryType { get; set; }
}
