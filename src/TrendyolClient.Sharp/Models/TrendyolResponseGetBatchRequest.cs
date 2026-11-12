using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetBatchRequest
{
    [JsonPropertyName("batchRequestId")]
    public string BatchRequestId { get; set; }

    /// <summary>
    ///     COMPLETED or IN_PROGRESS. Not returned for stock/price updates; check the item statuses instead.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("creationDate")]
    public long CreationDate { get; set; }

    [JsonPropertyName("lastModification")]
    public long LastModification { get; set; }

    /// <summary>
    ///     API or WEB.
    /// </summary>
    [JsonPropertyName("sourceType")]
    public string SourceType { get; set; }

    [JsonPropertyName("itemCount")]
    public int ItemCount { get; set; }

    [JsonPropertyName("failedItemCount")]
    public int FailedItemCount { get; set; }

    /// <summary>
    ///     ProductV2OnBoarding, ProductV2Update, ApprovedProductContentUpdate, ApprovedProductVariantUpdate, ProductDeliveryOptionUpdate, ProductInventoryUpdate, ProductArchiveUpdate, ProductDeletion, ProductUnlockUpdate.
    /// </summary>
    [JsonPropertyName("batchRequestType")]
    public string BatchRequestType { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolBatchRequestItem> Items { get; set; }
}
