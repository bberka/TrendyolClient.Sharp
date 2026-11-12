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
        if (request.Headers.TryGetValues("X-Internal-Seller-Id", out var sellerIdValues))
        {
            var sellerId = sellerIdValues.FirstOrDefault();
            var uriBuilder = new UriBuilder(request.RequestUri!);
            uriBuilder.Path = uriBuilder.Path.Replace("SELLER_ID_PARAMETER", sellerId);
            request.RequestUri = uriBuilder.Uri;

            // Some endpoints (e.g. test orders) take the seller id as a header, declared with the same placeholder.
            if (request.Headers.TryGetValues("SellerID", out var headerValues) && headerValues.Any(x => x.Contains("SELLER_ID_PARAMETER")))
            {
                request.Headers.Remove("SellerID");
                request.Headers.TryAddWithoutValidation("SellerID", sellerId);
            }
        }

        return await base.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
    }
}