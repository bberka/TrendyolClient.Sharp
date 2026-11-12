using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolDigitalDeliveryParams
{
    /// <summary>
    ///     6 to 120 characters; sent to the customer by SMS and e-mail.
    /// </summary>
    [JsonPropertyName("digitalCode")]
    public string DigitalCode { get; set; }
}
