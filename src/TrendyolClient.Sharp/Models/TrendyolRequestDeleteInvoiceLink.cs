using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestDeleteInvoiceLink
{
    [JsonPropertyName("serviceSourceId")]
    public long ServiceSourceId { get; set; }

    [JsonPropertyName("channelId")]
    public int ChannelId { get; set; }

    [JsonPropertyName("customerId")]
    public long CustomerId { get; set; }
}