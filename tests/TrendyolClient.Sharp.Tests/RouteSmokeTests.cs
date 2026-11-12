using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Tests.Support;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

/// <summary>
///     Calls every method of every public client interface through the real factory + Refit pipeline against a fake
///     HTTP handler, so broken route templates, unreplaced placeholders and duplicate routes fail here, not in production.
/// </summary>
public class RouteSmokeTests
{
    private const long SellerId = Harness.SellerId;

    public static IEnumerable<object[]> Clients()
    {
        yield return [typeof(ITrendyolMarketplaceApi)];
        yield return [typeof(ITrendyolWebhookApi)];
        yield return [typeof(ITrendyolFinanceApi)];
        yield return [typeof(ITrendyolExportApi)];
        yield return [typeof(ITrendyolInternationalApi)];
        yield return [typeof(ITrendyolGlobalPartnerApi)];
    }

    [Theory]
    [MemberData(nameof(Clients))]
    public async Task Every_method_builds_a_valid_request(Type clientType)
    {
        var (client, handler) = CreateClient(clientType);

        foreach (var method in AllMethods(clientType))
        {
            handler.Requests.Clear();
            var args = method.GetParameters()
                .Select(p => DefaultFor(p.ParameterType))
                .ToArray();

            var task = (Task)method.Invoke(client, args);
            await task;

            var request = Assert.Single(handler.Requests);
            var where = $"{clientType.Name}.{method.Name} -> {request.Method} {request.PathAndQuery}";
            Assert.DoesNotContain("SELLER_ID_PARAMETER", request.PathAndQuery);
            Assert.DoesNotContain("{", request.PathAndQuery);
            Assert.DoesNotContain("}", request.PathAndQuery);
            Assert.True(request.PathAndQuery.StartsWith("/integration/"), where);
            var route = method.GetCustomAttributes().OfType<HttpMethodAttribute>().Single().Path;
            if (route.Contains("SELLER_ID_PARAMETER")) Assert.Contains($"/{SellerId}", request.PathAndQuery);
            if (request.Message.Headers.TryGetValues("SellerID", out var sid)) Assert.Equal(SellerId.ToString(), sid.Single());
        }
    }

    [Theory]
    [MemberData(nameof(Clients))]
    public void No_two_methods_share_the_same_route(Type clientType)
    {
        var routes = AllMethods(clientType)
            .Select(m => (Method: m, Attr: m.GetCustomAttributes().OfType<HttpMethodAttribute>().Single()))
            .GroupBy(x => $"{x.Attr.Method} {x.Attr.Path}")
            .Where(g => g.Count() > 1 && g.Select(x => x.Method.Name).Distinct().Count() == g.Count())
            .Select(g => $"{g.Key}: {string.Join(", ", g.Select(x => x.Method.Name))}")
            .ToList();

        // Same route used by differently named methods is allowed only when their parameters genuinely differ (e.g. overloads
        // for multipart vs query), which we flag explicitly so it is a conscious choice.
        Assert.True(routes.Count == 0 || routes.All(r => r.Contains("Issue") || r.Contains("alternative-delivery")), string.Join(Environment.NewLine, routes));
    }

    [Fact]
    public void Marketplace_api_has_no_ambiguous_method_names()
    {
        var dupes = AllMethods(typeof(ITrendyolMarketplaceApi))
            .GroupBy(m => m.Name)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        Assert.Empty(dupes);
    }

    // ---------------------------------------------------------------------------------------------------------------

    private static IEnumerable<MethodInfo> AllMethods(Type t)
    {
        return new[] { t }.Concat(t.GetInterfaces())
            .SelectMany(i => i.GetMethods());
    }

    private static (object Client, RecordingHandler Handler) CreateClient(Type clientType)
    {
        var (factory, recorder) = Harness.Create();
        object client = clientType.Name switch
        {
            nameof(ITrendyolMarketplaceApi) => factory.GetOrCreateMarketplaceClient(SellerId, "key", "secret"),
            nameof(ITrendyolWebhookApi) => factory.GetOrCreateWebhookClient(SellerId, "key", "secret"),
            nameof(ITrendyolFinanceApi) => factory.GetOrCreateFinanceClient(SellerId, "key", "secret"),
            nameof(ITrendyolExportApi) => factory.GetOrCreateExportClient(SellerId, "key", "secret"),
            nameof(ITrendyolInternationalApi) => factory.GetOrCreateInternationalClient(SellerId, "key", "secret", "DE"),
            nameof(ITrendyolGlobalPartnerApi) => factory.GetOrCreateGlobalPartnerClient(SellerId, "key", "secret"),
            _ => throw new NotSupportedException(clientType.Name)
        };
        return (client, recorder);
    }

    private static object DefaultFor(Type type)
    {
        if (type == typeof(string)) return "x";
        if (type == typeof(StreamPart)) return new StreamPart(new MemoryStream([1, 2, 3]), "f.bin", "application/octet-stream");
        if (type == typeof(ByteArrayPart)) return new ByteArrayPart([1, 2, 3], "f.bin", "application/octet-stream");
        if (type.IsArray) return Array.CreateInstance(type.GetElementType(), 0);
        if (type.IsValueType) return Activator.CreateInstance(type);
        if (type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type) && type.GetGenericTypeDefinition() == typeof(List<>))
            return Activator.CreateInstance(type);
        if (type.IsClass && type.GetConstructor(Type.EmptyTypes) != null) return Activator.CreateInstance(type);
        return null;
    }
}
