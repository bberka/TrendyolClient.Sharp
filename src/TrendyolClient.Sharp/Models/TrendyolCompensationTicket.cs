using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCompensationTicket
{
    [JsonPropertyName("cargoProvider")]
    public string CargoProvider { get; set; }

    [JsonPropertyName("compensateReason")]
    public string CompensateReason { get; set; }

    /// <summary>Timestamp (ms).</summary>
    [JsonPropertyName("createDate")]
    public long CreateDate { get; set; }

    /// <summary>For example MarkInCompensation, CompensationApproved, NotCompensationCase, FoundInCompensation; see stateMessage for the description.</summary>
    [JsonPropertyName("currentState")]
    public string CurrentState { get; set; }

    [JsonPropertyName("deliveryNumber")]
    public string DeliveryNumber { get; set; }

    [JsonPropertyName("itemDetails")]
    public List<TrendyolCompensationItemDetail> ItemDetails { get; set; }

    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    [JsonPropertyName("requestedBy")]
    public string RequestedBy { get; set; }

    [JsonPropertyName("stateMessage")]
    public string StateMessage { get; set; }

    [JsonPropertyName("totalItemsAmount")]
    public string TotalItemsAmount { get; set; }
}
