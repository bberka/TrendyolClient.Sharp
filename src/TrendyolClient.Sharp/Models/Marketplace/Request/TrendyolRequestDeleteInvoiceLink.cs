namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestDeleteInvoiceLink
{
  /// <summary>
  ///   shipmentPackageId
  /// </summary>
  public long ServiceSourceId { get; set; }

  /// <summary>
  ///   Her zaman 1 olarak gönderilmelidir.
  /// </summary>
  public long ChannelId => 1;

  /// <summary>
  ///   sipariş paketleri çekme servisinden kontrol edilmelidir.
  /// </summary>
  public long CustomerId { get; set; }
}