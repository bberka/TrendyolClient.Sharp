using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     International Marketplace returned order (claim) services. The storefront of the target country is set once when the
///     client is created (<c>GetOrCreateInternationalClient</c>) and sent as the <c>storeFrontCode</c> header. Regions: DE = Germany, CEE = Central and Eastern Europe, GULF = Gulf countries. The Europe methods
///     (DE and CEE; only getClaims and as-accepted are documented for Germany) use the same paths as the Turkey claim service, the GULF methods use dedicated <c>-gulf</c> paths.
///     Services that are identical to <see cref="ITrendyolClaimApi" /> are not repeated here (see the notes on each method).
/// </summary>
public interface ITrendyolInternationalClaimApi
{

    /// <summary>
    ///     Region: DE, CEE. Lists returned orders ordered by lastModifiedDate. Differs from the Turkey service by the required
    ///     storeFrontCode header and by the response (BoxNow locker fields, SGR fee, accept detail, rejected package phone).
    ///     Fetch new returns with ClaimItemStatus = Created (the DE docs page says Accepted). When ClaimIds is set all other filters are ignored.
    /// </summary>
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/claims")]
    Task<IApiResponse<TrendyolIntlResponseGetClaims>> GetClaimsEuropeAsync(
        [Query] TrendyolFilterGetClaims filter);

    /// <summary>
    ///     Region: GULF. Lists returned orders ordered by lastModifiedDate. Same request and response as <see cref="GetClaimsEuropeAsync" />
    ///     but on the dedicated claims-gulf path; replacementOutboundPackageInfo is filled when the return is an exchange.
    /// </summary>
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/claims-gulf")]
    Task<IApiResponse<TrendyolIntlResponseGetClaims>> GetClaimsGulfAsync(
        [Query] TrendyolFilterGetClaims filter);

    /// <summary>
    ///     Region: DE, CEE. Creates a return that is accepted automatically (as-accepted) for a package that arrived without a return code.
    ///     Use it at the end of the refund process. The only claim creation service documented for Germany.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/claims/as-accepted")]
    Task<IApiResponse<TrendyolIntlResponseCreateClaimAsAccepted>> CreateClaimAsAcceptedEuropeAsync(
        [Body] TrendyolIntlRequestCreateClaimAsAccepted request);

    /// <summary>
    ///     Region: GULF. Creates a return in the Created status for a package that arrived without a return code. Body and response are the same
    ///     as <see cref="CreateClaimEuropeAsync" />; the OpenAPI spec puts it on the create-gulf path (the GULF docs page still prints claims/create).
    ///     shipmentCompanyId is 100 by default.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/claims/create-gulf")]
    Task<IApiResponse<TrendyolResponseCreateClaim>> CreateClaimGulfAsync(
        [Body] TrendyolRequestCreateClaim request);

    /// <summary>
    ///     Region: CEE. Returns the status transition history of a claim item. Needs the storeFrontCode header; executorUser is read as text.
    /// </summary>
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/claims/items/{claimItemsId}/audit")]
    Task<IApiResponse<List<TrendyolIntlClaimAuditItem>>> GetClaimItemAuditsEuropeAsync(
        [AliasAs("claimItemsId")] string claimItemsId);

    /// <summary>
    ///     Region: CEE. Notifies Trendyol of the tracking number of the package you ship back after a rejected return
    ///     (rejectedPackageInfo.packageId from the claims response). Seller-pays cargo only.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/tracking-details-rejected-europe")]
    Task<IApiResponse> UpdateRejectedTrackingNumberEuropeAsync(
        [AliasAs("packageId")] long packageId, [Body] TrendyolIntlRequestUpdateRejectedTrackingNumber request);

    /// <summary>
    ///     Region: GULF. Approves claim items that are in the WaitingInAction status. The request is the same as the Turkey service;
    ///     the docs additionally require the storeFrontCode header.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/claims/{claimId}/items/approve-gulf")]
    Task<IApiResponse> ApproveClaimLineItemsGulfAsync(
        [AliasAs("claimId")] string claimId, [Body] TrendyolRequestApproveClaim request);

    /// <summary>
    ///     Region: GULF. Rejects claim items (WaitingInAction only) without attachments; allowed only for reasons 1651, 451 and 2101.
    ///     claimItemIdList is comma separated, description is free text up to 500 characters. Reason ids come from
    ///     <see cref="GetClaimIssueReasonsGulfAsync" />. The docs print the three values as query parameters, the OpenAPI spec as form fields; the server reads query.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/claims/{claimId}/issue-gulf")]
    Task<IApiResponse> CreateClaimIssueGulfAsync(
        [AliasAs("claimId")] string claimId, [Query] long claimIssueReasonId, [Query] string claimItemIdList, [Query] string description);

    /// <summary>
    ///     Region: GULF. Rejects claim items (WaitingInAction only) with attachments sent as multipart field files (jpeg/jpg, png, pdf, tiff,
    ///     doc, docx, mp4, quicktime; 5 MB per file). A file is mandatory for every reason except 1651, 451 and 2101.
    /// </summary>
    [Multipart]
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/claims/{claimId}/issue-gulf")]
    Task<IApiResponse> CreateClaimIssueWithFilesGulfAsync(
        [AliasAs("claimId")] string claimId, [Query] long claimIssueReasonId, [Query] string claimItemIdList, [Query] string description, [AliasAs("files")] IEnumerable<ByteArrayPart> files);

    /// <summary>
    ///     Region: GULF. Lists the rejection reasons accepted by the claim issue service for the storefront (CEE and GULF lists differ from the Turkey one;
    ///     451 and 2051 are CEE only). Reason 1651 cannot be selected during the first 24 hours after the claim reached WaitingInAction.
    /// </summary>
    [Get("/integration/order/claim-issue-reasons-gulf")]
    Task<IApiResponse<List<TrendyolClaimIssueReason>>> GetClaimIssueReasonsGulfAsync();

    /// <summary>
    ///     Region: GULF. Returns the status transition history of a claim item. Needs the storeFrontCode header; executorUser is read as text.
    /// </summary>
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/claims/items/{claimItemsId}/audit-gulf")]
    Task<IApiResponse<List<TrendyolIntlClaimAuditItem>>> GetClaimItemAuditsGulfAsync(
        [AliasAs("claimItemsId")] string claimItemsId);

    /// <summary>
    ///     Region: GULF. Notifies Trendyol of the tracking number of the package you ship back after a rejected return
    ///     (rejectedPackageInfo.packageId from the claims response). Seller-pays cargo only.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/tracking-details-rejected-gulf")]
    Task<IApiResponse> UpdateRejectedTrackingNumberGulfAsync(
        [AliasAs("packageId")] long packageId, [Body] TrendyolIntlRequestUpdateRejectedTrackingNumber request);
}
