using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolUnapprovedProduct
{
    [JsonPropertyName("supplierId")]
    public long SupplierId { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    /// <summary>
    ///     rejected or pendingApproval.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("createDateTime")]
    public long CreateDateTime { get; set; }

    [JsonPropertyName("lastUpdateDate")]
    public long LastUpdateDate { get; set; }

    [JsonPropertyName("lastPriceChangeDate")]
    public long? LastPriceChangeDate { get; set; }

    [JsonPropertyName("lastStockChangeDate")]
    public long? LastStockChangeDate { get; set; }

    [JsonPropertyName("brand")]
    public TrendyolBrand Brand { get; set; }

    [JsonPropertyName("category")]
    public TrendyolCategory Category { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("listPrice")]
    public decimal ListPrice { get; set; }

    [JsonPropertyName("salePrice")]
    public decimal SalePrice { get; set; }

    [JsonPropertyName("cargoProviders")]
    public List<string> CargoProviders { get; set; }

    /// <summary>
    ///     When pendingApproval and true, upload the required document from the seller panel.
    /// </summary>
    [JsonPropertyName("docNeeded")]
    public bool DocNeeded { get; set; }

    [JsonPropertyName("vatRate")]
    public int VatRate { get; set; }

    [JsonPropertyName("dimensionalWeight")]
    public decimal? DimensionalWeight { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("media")]
    public List<TrendyolProductImage> Media { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolProductAttributeDetail> Attributes { get; set; }

    [JsonPropertyName("rejectReasonDetails")]
    public List<TrendyolRejectReasonDetail> RejectReasonDetails { get; set; }

    [JsonPropertyName("locationBasedDelivery")]
    public string LocationBasedDelivery { get; set; }

    [JsonPropertyName("lotNumber")]
    public string LotNumber { get; set; }
}
