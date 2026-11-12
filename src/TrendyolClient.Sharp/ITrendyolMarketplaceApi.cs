using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

public interface ITrendyolMarketplaceApi : ITrendyolProductApi, ITrendyolOrderApi, ITrendyolCommonLabelApi, ITrendyolClaimApi, ITrendyolQnAApi,
    ITrendyolMemberApi, ITrendyolSellerApi, ITrendyolInvoiceApi, ITrendyolCompensationApi
{
}
