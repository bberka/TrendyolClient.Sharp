namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolAnswer
{
  public long CreationDate { get; set; }
  public bool HasPrivateInfo { get; set; }
  public long Id { get; set; }

  public string Reason { get; set; }
  public string Text { get; set; }
}