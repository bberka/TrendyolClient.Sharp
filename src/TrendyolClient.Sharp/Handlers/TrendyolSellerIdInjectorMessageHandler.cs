using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TrendyolClient.Sharp.Handlers;

internal sealed class TrendyolSellerIdInjectorMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Headers.TryGetValues(TrendyolInternals.SellerIdHeader, out var sellerIdValues))
        {
            var sellerId = sellerIdValues.FirstOrDefault();
            var uriBuilder = new UriBuilder(request.RequestUri!);
            uriBuilder.Path = uriBuilder.Path.Replace(TrendyolInternals.SellerIdToken, sellerId);
            request.RequestUri = uriBuilder.Uri;

            // Some endpoints (e.g. test orders) take the seller id as a header, declared with the same placeholder.
            if (request.Headers.TryGetValues(TrendyolInternals.SellerIdRequestHeader, out var headerValues) && headerValues.Any(x => x.Contains(TrendyolInternals.SellerIdToken)))
            {
                request.Headers.Remove(TrendyolInternals.SellerIdRequestHeader);
                request.Headers.TryAddWithoutValidation(TrendyolInternals.SellerIdRequestHeader, sellerId);
            }
        }

        return await base.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
    }
}