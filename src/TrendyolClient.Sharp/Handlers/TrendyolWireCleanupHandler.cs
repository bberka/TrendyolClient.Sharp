using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TrendyolClient.Sharp.Handlers;

/// <summary>
///     The primary handler's wrapper, so it runs after every other handler, including ones the consumer adds: it removes the internal
///     seller id header, which the handlers above it read, so it is not sent to Trendyol.
/// </summary>
internal sealed class TrendyolWireCleanupHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Remove(TrendyolInternals.SellerIdHeader);
        return base.SendAsync(request, cancellationToken);
    }
}
