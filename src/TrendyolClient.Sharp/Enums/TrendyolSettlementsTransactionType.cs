namespace TrendyolClient.Sharp.Enums;

/// <summary>
///     Transaction types accepted by the settlements service. Names are sent to the API as-is.
///     SellerRevenue* and Commission* records belong together (e.g. SellerRevenuePositive with CommissionNegative).
/// </summary>
public enum TrendyolSettlementsTransactionType
{
    Sale,
    Return,
    Discount,
    DiscountCancel,
    Coupon,
    CouponCancel,
    ProvisionPositive,
    ProvisionNegative,
    ManualRefund,
    ManualRefundCancel,
    TyDiscount,
    TyDiscountCancel,
    TyCoupon,
    TyCouponCancel,
    SellerRevenuePositive,
    SellerRevenueNegative,
    CommissionPositive,
    CommissionNegative,
    SellerRevenuePositiveCancel,
    SellerRevenueNegativeCancel,
    CommissionPositiveCancel,
    CommissionNegativeCancel,
    DeliveryFee,
    DeliveryFeeCancel,
    PayByLink
}
