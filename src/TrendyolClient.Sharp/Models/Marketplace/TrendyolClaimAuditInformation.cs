namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolClaimAuditInformation
{
    public string ClaimId { get; set; }
    public string ClaimItemId { get; set; }
    public string PreviousStatus { get; set; }
    public string NewStatus { get; set; }
    public TrendyolExecutorUserInfoDocument UserInfoDocument { get; set; }
    public long Date { get; set; }
}