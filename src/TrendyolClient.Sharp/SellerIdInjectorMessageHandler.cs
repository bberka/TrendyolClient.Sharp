using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TrendyolClient.Sharp;

internal sealed class SellerIdInjectorMessageHandler : DelegatingHandler
{
  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
    if (request.Headers.TryGetValues("X-Internal-Seller-Id", out var sellerIdValues)) {
      var sellerId = sellerIdValues.FirstOrDefault();
      var uriBuilder = new UriBuilder(request.RequestUri!);
      uriBuilder.Path = uriBuilder.Path.Replace("SELLER_ID_PARAMETER", sellerId);
      request.RequestUri = uriBuilder.Uri;
    }
    
    return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
  }
}