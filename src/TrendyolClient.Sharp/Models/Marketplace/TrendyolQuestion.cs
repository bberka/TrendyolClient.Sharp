namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolQuestion
{
  public TrendyolAnswer Answer { get; set; }
  public string AnsweredDateMessage { get; set; }
  public long CreationDate { get; set; }
  public long CustomerId { get; set; }
  public long Id { get; set; }
  public string ImageUrl { get; set; }
  public string ProductName { get; set; }
  public bool Public { get; set; }
  public string Reason { get; set; }
  public TrendyolAnswer RejectedAnswer { get; set; }
  public long RejectedDate { get; set; }
  public string ReportReason { get; set; }
  public long ReportedDate { get; set; }
  public bool ShowUserName { get; set; }
  public string Status { get; set; }
  public string Text { get; set; }
  public string UserName { get; set; }
  public string WebUrl { get; set; }
}