using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCustomer
{
    [JsonPropertyName("customerFirstName")]
    public string CustomerFirstName { get; set; }

    [JsonPropertyName("customerLastName")]
    public string CustomerLastName { get; set; }
}