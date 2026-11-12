namespace TrendyolClient.Sharp.Enums;

/// <summary>
///     Rejection reason ids accepted by createClaimIssue (as returned by getClaimIssueReasons), valid since 8 Oct 2026.
///     Reasons 251, 1701, 1751, 2151 and 2201 were deactivated on that date and are rejected by the API.
///     Every reason except <see cref="ReturnPackageNotDelivered" />, <see cref="SendForAnalysis" /> and
///     <see cref="ExchangeFromOrderQuestion" /> requires at least one attached file.
/// </summary>
public enum TrendyolClaimIssueReasonId
{
    UsedOrDamagedByCustomer = 51,
    MissingProductOrQuantity = 401,
    MissingPartOrAccessory = 151,
    WrongProductReturned = 201,
    SendForAnalysis = 451,
    ProtectivePackagingOpened = 2051,
    CorporateReturnInvoiceMissingOrWrong = 1951,
    ReturnPackageNotDelivered = 1651,
    PersonalizedProduct = 2077,
    NoDefectAcceptedForSatisfaction = 2078,
    SentCorrectProductReturnedMissingDamagedOrDifferent = 2079,
    ExchangeFromOrderQuestion = 2101,
    NoProductionIssueFound = 1801,
    ProductExchanged = 1851,
    ProductRepaired = 1901
}
