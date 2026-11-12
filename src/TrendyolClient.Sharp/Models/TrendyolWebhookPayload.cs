namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Body Trendyol POSTs to a webhook URL: the full shipment package, identical to a getShipmentPackages item.
///     The subscribed status that triggered the call is in <see cref="TrendyolOrderPackage.Status" />.
///     Delivery is at-least-once, so handlers must be idempotent.
///     https://developers.trendyol.com/docs/webhook-model
/// </summary>
public class TrendyolWebhookPayload : TrendyolOrderPackage
{
}
