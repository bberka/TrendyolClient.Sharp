using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models.Finance;

public sealed class SettlementTransactionData
{
  public string Id { get; set; }
  public long TransactionDate { get; set; }
  public string Barcode { get; set; }
  public SettlementsTransactionType TransactionType { get; set; }
  public long ReceiptId { get; set; }
  public string Description { get; set; }
  public double Debt { get; set; }
  public double Credit { get; set; }
  public long PaymentPeriod { get; set; }
  public double CommissionRate { get; set; }
  public double CommissionAmount { get; set; }
  public double SellerRevenue { get; set; }
  public long OrderNumber { get; set; }
  public long PaymentOrderId { get; set; }
  public long PaymentDate { get; set; }
  public string CommissionInvoiceSerialNumber { get; set; }
  public long SellerId { get; set; }
  public string StoreId { get; set; }
  public string StoreName { get; set; }
  public string StoreAddress { get; set; }
}