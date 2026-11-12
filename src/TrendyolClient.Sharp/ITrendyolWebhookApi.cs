using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Trendyol Webhook API endpoints for managing webhook subscriptions
///     https://developers.trendyol.com/docs/marketplace/webhook-entegrasyonu
/// </summary>
public interface ITrendyolWebhookApi
{
    /// <summary>
    ///     Creates a webhook for shipment package status changes. Trendyol POSTs the full package
    ///     (<see cref="TrendyolWebhookPayload" />) to the URL. At most 15 webhooks per seller (passive ones count).
    ///     An empty status list subscribes to every status.
    /// </summary>
    /// <param name="request">Webhook configuration including URL, auth, and subscribed statuses</param>
    /// <returns>The id of the created webhook</returns>
    [Post("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks")]
    Task<IApiResponse<TrendyolResponseCreateWebhook>> CreateWebhookAsync([Body] TrendyolRequestCreateWebhook request);

    /// <summary>
    ///     Retrieves all webhook subscriptions for the seller.
    /// </summary>
    /// <returns>All configured webhooks, active and passive</returns>
    [Get("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks")]
    Task<IApiResponse<List<TrendyolWebhook>>> GetWebhooksAsync();

    /// <summary>
    ///     Updates an existing webhook subscription (URL, authentication, subscribed statuses).
    /// </summary>
    /// <param name="webhookId">The ID of the webhook to update</param>
    /// <param name="request">Updated webhook configuration</param>
    [Put("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks/{webhookId}")]
    Task<IApiResponse> UpdateWebhookAsync(
        [AliasAs("webhookId")] string webhookId,
        [Body] TrendyolRequestUpdateWebhook request
    );

    /// <summary>
    ///     Deletes a webhook subscription permanently.
    /// </summary>
    /// <param name="webhookId">The ID of the webhook to delete</param>
    /// <returns>API response indicating success or failure</returns>
    [Delete("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks/{webhookId}")]
    Task<IApiResponse> DeleteWebhookAsync([AliasAs("webhookId")] string webhookId);

    /// <summary>
    ///     Activates a passive webhook (Trendyol passivates webhooks that keep failing).
    /// </summary>
    /// <param name="webhookId">The ID of the webhook to activate</param>
    /// <returns>API response indicating success or failure</returns>
    [Put("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks/{webhookId}/activate")]
    Task<IApiResponse> ActivateWebhookAsync([AliasAs("webhookId")] string webhookId);

    /// <summary>
    ///     Deactivates a webhook subscription to stop receiving notifications.
    ///     The webhook configuration is preserved and can be reactivated later.
    /// </summary>
    /// <param name="webhookId">The ID of the webhook to deactivate</param>
    /// <returns>API response indicating success or failure</returns>
    [Put("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks/{webhookId}/deactivate")]
    Task<IApiResponse> DeactivateWebhookAsync([AliasAs("webhookId")] string webhookId);
}