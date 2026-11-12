using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

public interface ITrendyolClaimApi
{
    /// <summary>
    ///     Lists returned orders (claims), ordered by lastModifiedDate. Fetch new returns with
    ///     <c>ClaimItemStatus = Created</c>. When <c>ClaimIds</c> is set all other filters are ignored.
    /// </summary>
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/claims")]
    Task<IApiResponse<TrendyolResponseGetClaims>> GetClaimsAsync([Query] TrendyolFilterGetClaims filter);

    /// <summary>
    ///     Creates a return for a package that arrived without a return code. The claim is created in the Created status.
    ///     Use it only for returns you have already approved.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/claims/create")]
    Task<IApiResponse<TrendyolResponseCreateClaim>> CreateClaimAsync([Body] TrendyolRequestCreateClaim request);

    /// <summary>
    ///     Approves claim items. Only items in the WaitingInAction status can be approved; approved items may pass through
    ///     WaitingFraudCheck before they become Accepted.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/claims/{claimId}/items/approve")]
    Task<IApiResponse> ApproveClaimLineItemsAsync([AliasAs("claimId")] string claimId, [Body] TrendyolRequestApproveClaim request);

    /// <summary>
    ///     Rejects claim items (WaitingInAction only) without attachments. Allowed only for the reasons that need no file:
    ///     1651, 451 and 2101; every other reason needs <see cref="CreateClaimIssueWithFilesAsync" />.
    ///     <paramref name="claimItemIdList" /> is a comma-separated list of claim item ids, <paramref name="description" /> is
    ///     free text of at most 500 characters.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/claims/{claimId}/issue")]
    Task<IApiResponse> CreateClaimIssueAsync(
        [AliasAs("claimId")] string claimId,
        [Query] long claimIssueReasonId,
        [Query] string claimItemIdList,
        [Query] string description
    );

    /// <summary>
    ///     Rejects claim items (WaitingInAction only) with attachments sent as multipart form-data field <c>files</c>
    ///     (jpeg/jpg, png, pdf, tiff, doc, docx, mp4, quicktime; 5 MB per file).
    /// </summary>
    [Multipart]
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/claims/{claimId}/issue")]
    Task<IApiResponse> CreateClaimIssueWithFilesAsync(
        [AliasAs("claimId")] string claimId,
        [Query] long claimIssueReasonId,
        [Query] string claimItemIdList,
        [Query] string description,
        [AliasAs("files")] IEnumerable<ByteArrayPart> files
    );

    /// <summary>
    ///     Lists the rejection reasons accepted by createClaimIssue. Reason 1651 cannot be selected during the first 24 hours
    ///     after the claim reached WaitingInAction.
    /// </summary>
    [Get("/integration/order/claim-issue-reasons")]
    Task<IApiResponse<List<TrendyolClaimIssueReason>>> GetClaimIssueReasonsAsync();

    /// <summary>Returns the status transition history of a claim item (who changed it, from which app, when).</summary>
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/claims/items/{claimItemsId}/audit")]
    Task<IApiResponse<List<TrendyolClaimAuditItem>>> GetClaimItemAuditsAsync([AliasAs("claimItemsId")] string claimItemsId);
}
