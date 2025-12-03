using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Response;

public sealed class ResponseGetClaims
{
  public long TotalElements { get; set; }
  public long TotalPages { get; set; }
  public long Page { get; set; }
  public long Size { get; set; }
  public List<Claim> Content { get; set; }
}