using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models.Webhook.Request;
using TrendyolClient.Sharp.Models.Webhook.Response;

namespace TrendyolClient.Sharp;

/// <summary>
/// Trendyol Webhook API endpoints for managing webhook subscriptions
/// https://developers.trendyol.com/docs/marketplace/webhook-entegrasyonu
/// </summary>
public interface ITrendyolWebhookApi
{
    /// <summary>
    /// Creates a new webhook subscription for order status updates.
    /// The webhook will notify your endpoint when subscribed order statuses change.
    /// </summary>
    /// <param name="request">Webhook configuration including URL, auth, and subscribed statuses</param>
    /// <returns>Created webhook details including webhook ID</returns>
    [Post("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks")]
    Task<IApiResponse<TrendyolResponseWebhook>> CreateWebhookAsync([Body] TrendyolRequestCreateWebhook request);

    /// <summary>
    /// Retrieves all webhook subscriptions for the seller.
    /// </summary>
    /// <returns>List of all configured webhooks</returns>
    [Get("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks")]
    Task<IApiResponse<TrendyolResponseGetWebhooks>> GetWebhooksAsync();

    /// <summary>
    /// Updates an existing webhook subscription.
    /// You can change the URL, authentication method, or subscribed statuses.
    /// </summary>
    /// <param name="webhookId">The ID of the webhook to update</param>
    /// <param name="request">Updated webhook configuration</param>
    /// <returns>Updated webhook details</returns>
    [Put("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks/{webhookId}")]
    Task<IApiResponse<TrendyolResponseWebhook>> UpdateWebhookAsync(
        [AliasAs("webhookId")] string webhookId,
        [Body] TrendyolRequestUpdateWebhook request
    );

    /// <summary>
    /// Deletes a webhook subscription permanently.
    /// </summary>
    /// <param name="webhookId">The ID of the webhook to delete</param>
    /// <returns>API response indicating success or failure</returns>
    [Delete("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks/{webhookId}")]
    Task<IApiResponse> DeleteWebhookAsync([AliasAs("webhookId")] string webhookId);

    /// <summary>
    /// Activates a webhook subscription to start receiving notifications.
    /// </summary>
    /// <param name="webhookId">The ID of the webhook to activate</param>
    /// <returns>API response indicating success or failure</returns>
    [Put("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks/{webhookId}/activate")]
    Task<IApiResponse> ActivateWebhookAsync([AliasAs("webhookId")] string webhookId);

    /// <summary>
    /// Deactivates a webhook subscription to stop receiving notifications.
    /// The webhook configuration is preserved and can be reactivated later.
    /// </summary>
    /// <param name="webhookId">The ID of the webhook to deactivate</param>
    /// <returns>API response indicating success or failure</returns>
    [Put("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks/{webhookId}/deactivate")]
    Task<IApiResponse> DeactivateWebhookAsync([AliasAs("webhookId")] string webhookId);
}