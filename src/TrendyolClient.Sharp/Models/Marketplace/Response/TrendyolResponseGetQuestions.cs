using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Response;

public sealed class ResponseGetQuestions
{
  public List<Question> Content { get; set; }
  public long Page { get; set; }
  public long Size { get; set; }
  public long TotalElements { get; set; }
  public long TotalPages { get; set; }
}