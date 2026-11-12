using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetCompensationTickets
{
    [JsonPropertyName("data")]
    public List<TrendyolCompensationTicket> Data { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
}
