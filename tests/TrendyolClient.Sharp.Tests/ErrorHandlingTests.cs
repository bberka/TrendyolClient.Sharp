using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TrendyolClient.Sharp.Exceptions;
using TrendyolClient.Sharp.Tests.Support;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

public class ErrorHandlingTests
{
    private static (ITrendyolMarketplaceApi Client, RecordingHandler Transport) Build(bool throwOnError, HttpStatusCode status, string body = """{"errors":[{"message":"boom"}]}""",
        Action<HttpResponseMessage> tweak = null)
    {
        var transport = new RecordingHandler
        {
            Responder = _ =>
            {
                var response = RecordingHandler.Json(status, body);
                tweak?.Invoke(response);
                return response;
            }
        };

        var services = new ServiceCollection();
        services.AddTrendyolApiClient(c =>
        {
            c.ThrowOnError = throwOnError;
            c.EnableLogging = false;
        });
        // Replace the real network handler with the fake transport
        services.AddHttpClient("TrendyolClient").ConfigurePrimaryHttpMessageHandler(() => transport);

        var factory = services.BuildServiceProvider().GetRequiredService<TrendyolMarketplaceClientFactory>();
        return (factory.GetOrCreateMarketplaceClient(Harness.SellerId, "k", "s"), transport);
    }

    // ---- default: no exceptions, but the failure is observable ------------------------------------------------

    [Theory]
    [InlineData(HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task By_default_failures_are_returned_with_the_error_details(HttpStatusCode status)
    {
        var (client, _) = Build(false, status);

        var response = await client.GetCountriesAsync();

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(status, response.StatusCode);
        Assert.NotNull(response.Error);
        Assert.Contains("boom", response.Error.Content);
    }

    // ---- ThrowOnError: typed exceptions through the real DI pipeline ------------------------------------------

    [Fact]
    public async Task Unauthorized_throws_authentication_exception_with_the_seller_id()
    {
        var (client, _) = Build(true, HttpStatusCode.Unauthorized);

        var ex = await Assert.ThrowsAsync<TrendyolAuthenticationException>(() => client.GetCountriesAsync());

        Assert.Equal(Harness.SellerId, ex.SellerId);
    }

    [Fact]
    public async Task Forbidden_throws_authentication_exception_that_mentions_the_user_agent()
    {
        var (client, _) = Build(true, HttpStatusCode.Forbidden);

        var ex = await Assert.ThrowsAsync<TrendyolAuthenticationException>(() => client.GetCountriesAsync());

        Assert.Contains("User-Agent", ex.Message);
    }

    [Fact]
    public async Task Rate_limit_throws_with_the_retry_after_delay()
    {
        var (client, _) = Build(true, HttpStatusCode.TooManyRequests, tweak: r => r.Headers.RetryAfter = new System.Net.Http.Headers.RetryConditionHeaderValue(TimeSpan.FromSeconds(30)));

        var ex = await Assert.ThrowsAsync<TrendyolRateLimitException>(() => client.GetCountriesAsync());

        Assert.InRange(ex.RetryAfterSeconds, 25, 31);
    }

    [Fact]
    public async Task Rate_limit_without_header_falls_back_to_a_minute()
    {
        var (client, _) = Build(true, HttpStatusCode.TooManyRequests);

        var ex = await Assert.ThrowsAsync<TrendyolRateLimitException>(() => client.GetCountriesAsync());

        Assert.InRange(ex.RetryAfterSeconds, 55, 61);
    }

    [Fact]
    public async Task Bad_request_throws_validation_exception_carrying_the_body()
    {
        var (client, _) = Build(true, HttpStatusCode.BadRequest, """{"errors":[{"key":"barcode","message":"required"}]}""");

        var ex = await Assert.ThrowsAsync<TrendyolValidationException>(() => client.GetCountriesAsync());

        Assert.Contains("required", ex.Message);
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError, 500)]
    [InlineData(HttpStatusCode.BadGateway, 502)]
    [InlineData(HttpStatusCode.ServiceUnavailable, 503)]
    [InlineData(HttpStatusCode.GatewayTimeout, 504)]
    public async Task Server_errors_throw_server_exception_with_status_and_body(HttpStatusCode status, int code)
    {
        var (client, _) = Build(true, status, "upstream down");

        var ex = await Assert.ThrowsAsync<TrendyolServerException>(() => client.GetCountriesAsync());

        Assert.Equal(code, ex.StatusCode);
        Assert.Equal("upstream down", ex.ResponseBody);
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, typeof(TrendyolNotFoundException))]
    [InlineData(HttpStatusCode.Conflict, typeof(TrendyolConflictException))]
    [InlineData(HttpStatusCode.UnprocessableEntity, typeof(TrendyolBusinessRuleException))]
    [InlineData(HttpStatusCode.MethodNotAllowed, typeof(TrendyolRequestFailedException))]
    [InlineData(HttpStatusCode.UnsupportedMediaType, typeof(TrendyolRequestFailedException))]
    [InlineData(HttpStatusCode.RequestUriTooLong, typeof(TrendyolRequestFailedException))]
    public async Task Other_statuses_map_to_their_exception_type(HttpStatusCode status, Type expected)
    {
        var (client, _) = Build(true, status);

        var ex = await Assert.ThrowsAnyAsync<TrendyolException>(() => client.GetCountriesAsync());

        Assert.IsType(expected, ex);
    }

    [Fact]
    public async Task Success_never_throws_even_with_throw_on_error()
    {
        var (client, _) = Build(true, HttpStatusCode.OK, "[]");

        var response = await client.GetCountriesAsync();

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public void Exception_types_are_public_so_callers_can_catch_them()
    {
        var publicTypes = typeof(TrendyolException).Assembly.GetExportedTypes();
        foreach (var type in new[]
                 {
                     typeof(TrendyolException), typeof(TrendyolAuthenticationException), typeof(TrendyolRateLimitException),
                     typeof(TrendyolValidationException), typeof(TrendyolNotFoundException), typeof(TrendyolServerException)
                 })
            Assert.Contains(type, publicTypes);
    }
}
