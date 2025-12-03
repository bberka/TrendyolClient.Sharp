using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models;

public readonly struct TrendyolApiResult
{
  public bool IsSuccessStatusCode { get; }
  public int StatusCode { get; }
  public string ReasonPhrase { get; }
  public string Content { get; }
  public IReadOnlyDictionary<string, string> Headers { get; }
  public string RequestUrl { get; }
  public string RequestJsonBody { get; }

  public TrendyolApiResult(
    bool isSuccessStatusCode,
    int statusCode,
    string reasonPhrase,
    string content,
    IReadOnlyDictionary<string, string> headers,
    string requestUrl,
    string requestJsonBody = null) {
    IsSuccessStatusCode = isSuccessStatusCode;
    StatusCode = statusCode;
    ReasonPhrase = reasonPhrase;
    Content = content;
    Headers = headers;
    RequestUrl = requestUrl;
    RequestJsonBody = requestJsonBody;
  }

  internal TrendyolApiResult<T> WithData<T>(T data) {
    return new TrendyolApiResult<T>(IsSuccessStatusCode,
      StatusCode,
      ReasonPhrase,
      Content,
      Headers,
      data,
      RequestUrl,
      RequestJsonBody);
  }
}

public readonly struct TrendyolApiResult<T>
{
  public bool IsSuccessStatusCode { get; }
  public int StatusCode { get; }
  public string ReasonPhrase { get; }
  public string Content { get; }
  public T Data { get; }
  public IReadOnlyDictionary<string, string> Headers { get; }
  public string RequestUrl { get; }
  public string RequestJsonBody { get; }

  public TrendyolApiResult(
    bool isSuccessStatusCode,
    int statusCode,
    string reasonPhrase,
    string content,
    IReadOnlyDictionary<string, string> headers,
    T data,
    string requestUrl,
    string requestJsonBody = null) {
    IsSuccessStatusCode = isSuccessStatusCode;
    StatusCode = statusCode;
    ReasonPhrase = reasonPhrase;
    Content = content;
    Data = data;
    Headers = headers;
    RequestUrl = requestUrl;
    RequestJsonBody = requestJsonBody;
  }

  internal TrendyolApiResult ToApiResult() {
    return new TrendyolApiResult(IsSuccessStatusCode,
      StatusCode,
      ReasonPhrase,
      Content,
      Headers,
      RequestUrl,
      RequestJsonBody);
  }
}