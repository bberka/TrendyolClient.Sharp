using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Response;

public class TrendyolResponseGetBatchRequestResult
{
  public string BatchRequestId { get; set; }


  public List<TrendyolBatchRequestResultItem> Items { get; set; }


  public string Status { get; set; }


  public long CreationDate { get; set; }


  public long LastModification { get; set; }


  public string SourceType { get; set; }


  public long ItemCount { get; set; }
}