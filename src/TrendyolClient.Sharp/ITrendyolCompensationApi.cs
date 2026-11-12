using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

public interface ITrendyolCompensationApi
{
    /// <summary>
    ///     Lists compensation (tazmin) tickets of packages shipped with TEX, ordered by createDate descending.
    /// </summary>
    [Get("/integration/tex/compensation/sellers/SELLER_ID_PARAMETER/tickets")]
    Task<IApiResponse<TrendyolResponseGetCompensationTickets>> GetCompensationTicketsAsync([Query] TrendyolFilterGetCompensationTickets filter);
}
