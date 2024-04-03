﻿namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestDeleteInvoiceLink
{
  /// <summary>
  /// shipmentPackageId
  /// </summary>
  public long ServiceSourceId { get; set; }

  /// <summary>
  /// Her zaman 1 olarak gönderilmelidir.
  /// </summary>
  public int ChannelId => 1;

  /// <summary>
  /// sipariş paketleri çekme servisinden kontrol edilmelidir.
  /// </summary>
  public long CustomerId { get; set; }
}