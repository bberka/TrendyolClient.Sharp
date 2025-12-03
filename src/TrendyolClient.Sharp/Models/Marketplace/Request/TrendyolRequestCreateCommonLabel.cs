namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestCreateCommonLabel
{
  public string Format { get; set; } // required
  public long BoxQuantity { get; set; }
  public double VolumetricHeight { get; set; }
}