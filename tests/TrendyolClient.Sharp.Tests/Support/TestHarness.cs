using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TrendyolClient.Sharp.Tests.Support;

public sealed record SeenRequest(HttpMethod Method, string PathAndQuery, string Host, HttpRequestMessage Message, string Body);

/// <summary>Fake transport: records requests and answers with a configurable response.</summary>
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

public static class Harness
{
    public const long SellerId = 123456;

    /// <summary>
    ///     Builds the clients through the real <c>AddTrendyolApiClient</c> pipeline (every handler, in production order), with the network
    ///     replaced by a recording transport, so a test cannot pass against a pipeline that differs from the shipped one.
    /// </summary>
    public static (IServiceProvider Services, RecordingHandler Transport) CreateServices(
        TrendyolClientConfig config = null,
        Action<IHttpClientBuilder> customize = null,
        Action<IServiceCollection> register = null
    )
    {
        var transport = new RecordingHandler();
        var services = new ServiceCollection();
        services.AddLogging();
        var builder = services.AddTrendyolApiClient(c =>
        {
            if (config == null) return;
            c.IntegrationName = config.IntegrationName;
            c.RequestTimeoutSeconds = config.RequestTimeoutSeconds;
            c.EnableLogging = config.EnableLogging;
            c.ThrowOnError = config.ThrowOnError;
        });
        builder.ConfigurePrimaryHttpMessageHandler(() => TrendyolServiceCollectionExtensions.CreatePrimaryHandler(transport));
        customize?.Invoke(builder);
        register?.Invoke(services);
        return (services.BuildServiceProvider(), transport);
    }

    public static (TrendyolMarketplaceClientFactory Factory, RecordingHandler Transport) Create(TrendyolClientConfig config = null, Action<IHttpClientBuilder> customize = null)
    {
        var (services, transport) = CreateServices(config, customize);
        return (services.GetRequiredService<TrendyolMarketplaceClientFactory>(), transport);
    }
}
