namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestCreateCommonLabel
{
  public string Format { get; set; } // required
  public long BoxQuantity { get; set; }
  public double VolumetricHeight { get; set; }
}