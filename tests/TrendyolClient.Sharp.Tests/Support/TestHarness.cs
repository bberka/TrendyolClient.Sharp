using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Handlers;

namespace TrendyolClient.Sharp.Tests.Support;

public sealed record SeenRequest(HttpMethod Method, string PathAndQuery, string Host, HttpRequestMessage Message, string Body);

/// <summary>Fake transport: records requests and answers with a queued or default response.</summary>
public sealed class RecordingHandler : HttpMessageHandler
{
    public List<SeenRequest> Requests { get; } = [];
    public Func<HttpRequestMessage, HttpResponseMessage> Responder { get; set; } = _ => Json(HttpStatusCode.OK, "{}");

    public static HttpResponseMessage Json(HttpStatusCode status, string json)
    {
        return new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var body = request.Content == null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
        Requests.Add(new SeenRequest(request.Method, request.RequestUri.PathAndQuery, request.RequestUri.Host, request, body));
        var response = Responder(request);
        response.RequestMessage = request;
        return response;
    }
}

public sealed class FakeHttpClientFactory(HttpMessageHandler transport) : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
    {
        // Same handler order as the DI registration: seller-id injector in front of the transport.
        return new HttpClient(new TrendyolSellerIdInjectorMessageHandler { InnerHandler = transport }, false);
    }
}

public static class Harness
{
    public const long SellerId = 123456;

    public static (TrendyolMarketplaceClientFactory Factory, RecordingHandler Transport) Create(TrendyolClientConfig config = null)
    {
        var transport = new RecordingHandler();
        return (new TrendyolMarketplaceClientFactory(new FakeHttpClientFactory(transport), config ?? new TrendyolClientConfig()), transport);
    }
}
