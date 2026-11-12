using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalRequestCreateOrder
{
    /// <summary>
    ///     Order items (max 100)
    /// </summary>
    [JsonPropertyName("items")]
    public List<TrendyolGlobalRequestOrderItem> Items { get; set; }

    /// <summary>
    ///     Unique UUIDv4 reference for each order
    /// </summary>
    [JsonPropertyName("vendorRef")]
    public string VendorRef { get; set; }

    /// <summary>
    ///     Optional address title, used for communication with the account manager
    /// </summary>
    [JsonPropertyName("addressTitle")]
    public string AddressTitle { get; set; }
}
