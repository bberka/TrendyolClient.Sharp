namespace TrendyolClient.Sharp.Enums;

/// <summary>
///     https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-sebepleri
/// </summary>
public enum TrendyolReturnReason
{
    None = 51, //Trendyol
    WarehouseLoss = 101, //Trendyol
    CrossDefective = 151, //Trendyol
    CustomerLostReturn = 201, //Trendyol
    DislikeModel = 251, //Customer
    FaultyProductSent = 301, //Customer
    WrongProductSent = 351, //Customer
    ChangedMind = 401, //Customer
    Other = 451, //Customer
    SizeTooSmall = 501, //Customer
    SizeTooLarge = 551, //Customer
    ProductNotAsSpecified = 651, //Customer
    WrongOrderPlaced = 701, //Customer
    OtherFraudRelated = 751, //Trendyol
    DislikeQuality = 1651, //Customer
    DeliveryDelay = 1701, //Trendyol
    PenalApproval = 2000, //Trendyol
    NonPenalApproval = 2001, //Trendyol
    TechnicalSupportRequired = 2002, //Trendyol
    UsedProductRejected = 2003, //Trendyol
    HygienicReasonRejected = 2004, //Trendyol
    NoProductionIssueFromAnalysis = 2005, //Trendyol
    AnalysisExchange = 2006, //Trendyol
    AnalysisRepair = 2007, //Trendyol
    MissingQuantityOrAccessory = 2008, //Trendyol
    NonFirmProduct = 2009, // Non-Firm Product
    Reshipment = 2010, //Trendyol
    InitiatedBySeller = 2011, //Trendyol
    ReturnPeriodExpired = 2012, //Trendyol
    ProductWithCustomer = 2013, //Trendyol
    SendForAnalysis = 2014, //Trendyol
    UndeliverableShipment = 2015, //Trendyol
    BetterPriceAvailable = 2030, // Better Price Available
    DontLikeProduct = 2042, // Don't Like Product
    MissingPartOfProduct = 2043 // Missing Part Of Product
}