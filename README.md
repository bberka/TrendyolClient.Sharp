

# TrendyolClient.Sharp

[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-512BD4)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Nuget](https://img.shields.io/badge/nuget-TrendyolClient.Sharp-blue)](https://www.nuget.org/packages/TrendyolClient.Sharp)

A modern, strongly-typed, and community-driven C\# client library for the [Trendyol API](https://developers.trendyol.com/). Built on top of **Refit**, this library provides a robust wrapper for Marketplace, Finance, and Webhook endpoints with a focus on memory efficiency and developer experience.

> **⚠️ PROJECT STATUS: DEVELOPMENT**
>
> This project is maintained for bug fixes and issue resolution; however, it is **not yet considered production-ready**.
>
> - **Breaking Changes:** The public API may change significantly between versions.
> - **Validation:** The library does not validate input (see [Scope](#scope)); you must validate data before sending it.
> - **Use at your own risk.**

## Features

- **Modular Design:** Separate clients for Marketplace, Finance, Webhook, and Export Center (AutoFT) APIs.
- **Lazy Loading:** Uses `Lazy<T>` to instantiate specific clients only when accessed, reducing memory footprint.
- **Type Safety:** Full C\# strong typing for requests, responses, and enumerations.
- **Error mapping:** Optional mapping of Trendyol HTTP status codes to specific C\# exceptions (e.g., `TrendyolRateLimitException`); see [Error Handling](#error-handling).
- **DI Ready:** Seamless integration with ASP.NET Core `IServiceCollection`.
- **Multi-Environment:** Native support for switching between Production and Staging APIs.

## Scope

This package does one thing: it gives you **type-safe access to the Trendyol API**, following Trendyol's documentation and .NET best practices (typed requests and responses, `IHttpClientFactory`, dependency injection, Refit). It is a thin, faithful client, not a framework around the API.

**What it does not do, and never will:**

- **No input validation.** It sends what you give it. Trendyol validates requests and reports problems in the response; check the response (or enable `ThrowOnError`) to see them.
- **No rate-limit checks.** Trendyol allows 50 requests per 10 seconds per endpoint. Staying under that is your responsibility; the library does not throttle, queue or count requests.
- **No retries.** Failed calls are not repeated. Many calls are not safe to repeat (creating products, splitting packages, creating claims), and the right policy depends on your workload.
- **No business logic.** No caching of Trendyol data, no mapping to your domain model, no synchronization jobs.

Keeping these out keeps the library small, predictable and easy to keep in step with Trendyol's API. If you need them, add them around the client. Retries and throttling, for example, belong in the `HttpClient` pipeline: the clients are built on the named `HttpClient` `"TrendyolClient"`, so you can attach your own handler or resilience policy with `services.AddHttpClient("TrendyolClient")` after `AddTrendyolApiClient`.

## Supported frameworks

The package targets **.NET 8**, **.NET 9** and **.NET 10** (`net8.0`, `net9.0`, `net10.0`). Building from source needs the **.NET 10 SDK** (pinned in `global.json`); running the tests on all three targets needs the .NET 8, 9 and 10 runtimes.

## Installation

```bash
dotnet add package TrendyolClient.Sharp
```

## Quick Start

### 1\. Registration

Add the client factory to your dependency injection container in `Program.cs`.

```csharp
builder.Services.AddTrendyolApiClient(config =>
{
    config.IntegrationName = "MyCompanyIntegration"; // see "User-Agent" below
    config.EnableLogging = true;
    config.RequestTimeoutSeconds = 60;
    config.ThrowOnError = true; // optional: throw typed exceptions, see "Error Handling"
});
```

### 2\. Usage

Inject `TrendyolMarketplaceClientFactory` into your service. You can request specific clients (Marketplace, Webhook, Finance, or Export) based on your needs.

```csharp
public class ProductService
{
    private readonly TrendyolMarketplaceClientFactory _factory;

    public ProductService(TrendyolMarketplaceClientFactory factory)
    {
        _factory = factory;
    }

    public async Task SyncInventory(long sellerId, string apiKey, string apiSecret)
    {
        // 1. Get the Marketplace client
        // This leverages internal caching; calling this multiple times is cheap.
        var client = _factory.GetOrCreateMarketplaceClient(sellerId, apiKey, apiSecret);

        // 2. Make the API call (every method returns Refit's IApiResponse<T>)
        try
        {
            var response = await client.UpdatePriceAndInventoryAsync(new TrendyolRequestUpdatePriceAndInventory
            {
                Items = new List<TrendyolPriceAndInventoryItem>
                {
                    new() { Barcode = "123456789", Quantity = 50, SalePrice = 199.90m, ListPrice = 249.90m }
                }
            });

            // Product writes are asynchronous: poll the batch result with the returned id.
            var batch = await client.GetBatchRequestResultAsync(response.Content.BatchRequestId);
        }
        catch (TrendyolRateLimitException ex) // only thrown when ThrowOnError = true
        {
            // HTTP 429: Trendyol allows 50 requests per 10 seconds per endpoint
            Console.WriteLine($"Rate limited. Retry after: {ex.RetryAfter}");
        }
    }
}
```

## Architecture & Performance

This library uses a **Lazy-Loaded Container Pattern**.

When you call `GetOrCreateMarketplaceClient`, the factory:

1.  Checks an internal `ConcurrentDictionary` for an existing session for that `sellerId`.
2.  If found, it returns the client immediately.
3.  If not found, it creates a **ClientContainer**.

The `ClientContainer` initializes a single `HttpClient` shared across all contexts. However, the Refit proxies for Marketplace, Finance, Webhook, or Export are **only created when you access them**. If you only use the Finance API, the memory overhead for the other definitions is never allocated.

## API Coverage

Built against the Trendyol developer documentation as of November 2026 (**Product V2** and **Order V2** services).

### Marketplace client (`GetOrCreateMarketplaceClient`)

| Area | What you get |
| :--- | :--- |
| **Products (V2)** | Brands (list, by name, create), category tree, V2 category attributes and attribute values, create products (`/v2/products`), unapproved / approved content / variant / delivery-info bulk updates, price and inventory, delete, archive, unlock, buybox, batch result, update audits, filtering (base info, unapproved, approved, approved inventory-and-price), cargo providers, product videos |
| **Orders (V2)** | `GetOrderPackagesAsync` (`/v2/orders`) and the streaming variant, package status, unsupplied items, box info, delivered-by-service, cargo provider and warehouse changes, labor costs, four package-split variants, alternative delivery (link, phone, digital), manual deliver and return, test-order helpers for the stage environment |
| **Claims** | List, create, approve, reject with reason (with or without files), issue reasons, item audits |
| **Q&A** | Filter and get questions, answer, create test question (stage) |
| **Locations** | Countries, GULF/CEE cities and districts, Azerbaijan, and Türkiye cities, districts and neighborhoods |
| **Labels / Compensation** | Common label barcode request and retrieval (including AB product labels), compensation tickets |
| **Seller / Invoice** | Return, shipment and invoice addresses, warranty documents, test-environment IP whitelist, invoice link send/delete, invoice file upload |

### Finance client (`GetOrCreateFinanceClient`)

Settlements, other financials, payment orders, and cargo invoice items.

### Webhook client (`GetOrCreateWebhookClient`)

Create, list, update, delete, activate, and deactivate order webhooks. `TrendyolWebhookPayload` is the order package that Trendyol posts to your endpoint.

### Export client (`GetOrCreateExportClient`)

The Export Center (AutoFT, `/integration/ecgw`) API: create and list export products, lookups (origins, compositions, care instructions, category attributes, categories by barcode), stock and price updates, packages (V2/V3), and batch status. Every call requires the `x-clientip`, `x-correlationid`, and `x-agentname` headers, which are exposed as method parameters.

## Migrating from 1.x

Trendyol closed the **Product V1** and **Order V1** services on 15 October 2026, so the matching methods were removed rather than kept.

| Removed (1.x) | Use instead |
| :--- | :--- |
| `GetOrderPackagesAsync` on `/orders` | `GetOrderPackagesAsync` now calls `/v2/orders` (max 10,000 records per query window; narrow the date range or use `GetOrderPackagesStreamAsync`) |
| `GetProductsAsync` | `GetProductBaseAsync`, `GetUnapprovedProductsAsync`, `GetApprovedProductsAsync`, `GetApprovedProductsInventoryAndPriceAsync` |
| `UpdateProductsAsync` | `UpdateUnapprovedProductsAsync`, `UpdateContentBulkAsync`, `UpdateVariantBulkAsync`, `UpdateDeliveryInfoBulkAsync` |
| `CreateProductsAsync` (V1 payload) | `CreateProductsAsync` with the V2 payload (`TrendyolRequestCreateProduct`) |
| `GetCategoryAttributesAsync` (V1) | `GetCategoryAttributesAsync` (V2) and `GetCategoryAttributeValuesAsync` |
| `ApproveClaimAsync` / `GetClaimAuditAsync` / `RequestBarcodeAsync` | `ApproveClaimLineItemsAsync` / `GetClaimItemAuditsAsync` / `CreateCommonLabelAsync` |
| `GetAddressesAsync` | `GetSuppliersAddressesAsync` |
| `UpdateTrackingNumberAsync`, `ExtendAgreedDeliveryDateAsync` | Removed by Trendyol |

Other behavior changes worth knowing:

- Settlement and other-financials filters take `StartDate` / `EndDate` as Unix milliseconds (`long?`), not `DateTime`.
- Cargo tracking numbers are strings: Trendyol sends values larger than `long.MaxValue`.
- Webhook and order status enums now serialize with the API's `UPPER_SNAKE_CASE` names.
- Product-write endpoints return a `batchRequestId`; poll `GetBatchRequestResultAsync` for the outcome.
- The Export client is separate from the Marketplace client because it reuses names such as `CreateProductsAsync`.

## Error Handling

Every method returns Refit's `IApiResponse` / `IApiResponse<T>`. What happens on a non-success status depends on `TrendyolClientConfig.ThrowOnError`.

**`ThrowOnError = false` (default)** nothing is thrown. Check `IsSuccessStatusCode`; on failure `Error` holds the status code and the response body (Trendyol puts the reason there).

```csharp
var response = await client.GetCategoriesAsync();
if (!response.IsSuccessStatusCode)
    Console.WriteLine($"{(int)response.StatusCode}: {response.Error?.Content}");
```

**`ThrowOnError = true`** failures are mapped to the typed exceptions in `TrendyolClient.Sharp.Exceptions`, all deriving from `TrendyolException`:

| HTTP Code | Exception Type | Description |
| :-------- | :------------- | :---------- |
| `401` | `TrendyolAuthenticationException` | Invalid API key/secret (check that you are not mixing production and stage credentials). |
| `403` | `TrendyolAuthenticationException` | Wrong endpoint or parameters, or a request without a `User-Agent` header. |
| `400` | `TrendyolValidationException` | Invalid request; the message contains Trendyol's error body. |
| `404` | `TrendyolNotFoundException` | Resource not found. |
| `409` | `TrendyolConflictException` | The resource is not in the expected state. |
| `422` | `TrendyolBusinessRuleException` | Business rule violation. |
| `429` | `TrendyolRateLimitException` | Too many requests. `RetryAfter` is taken from the `Retry-After` header, or one minute from now. |
| `500` `502` `503` `504` | `TrendyolServerException` | Trendyol-side failure. Carries `StatusCode` and `ResponseBody`. Retry; on stage, `503` usually means your IP is not whitelisted. |
| other | `TrendyolRequestFailedException` | For example `405`, `414`, `415`. Carries `StatusCode` and `ResponseBody`. |

Trendyol allows **50 requests per 10 seconds per endpoint**; the 51st gets a `429`.

## Advanced Configuration

### Using the Staging Environment

Production (`apigw.trendyol.com`) needs no IP authorization. The staging environment (`stageapigw.trendyol.com`) does: Trendyol must whitelist your server's outbound IP first (a `503` from stage means it is not), and stage uses its own seller ID, API key and API secret, separate from production. Pass `useStageApi: true` to switch.

```csharp
var client = _factory.GetOrCreateMarketplaceClient(
    sellerId,
    apiKey,
    apiSecret,
    useStageApi: true // Points to stageapigw.trendyol.com
);
```

Production and stage clients for the same seller ID are cached separately.

### User-Agent

Trendyol rejects requests without a `User-Agent` (403). The library sends `"{sellerId} - {IntegrationName}"`. Use your integration company's name, or leave the default `SelfIntegration` when the software belongs to the seller. Trendyol requires alphanumeric characters only, at most 30.

### Request serialization

Request bodies use camelCase and leave out properties you did not set, so a partial update (for example prices without `Quantity`) does not overwrite the other fields. Enums that Trendyol documents in upper snake case (`TrendyolOrderStatus`, `TrendyolWebhookAuthenticationType`) are written as `AT_COLLECTION_POINT` / `BASIC_AUTHENTICATION`.

### Manual Cache Invalidation

If a seller regenerates their API credentials, a different key/secret pair already rebuilds the client automatically. To drop the cached clients of a seller explicitly (production and stage), call:

```csharp
_factory.InvalidateClient(sellerId);
```

## Contributing

We welcome contributions\! Since this is a community project:

1.  **Open an Issue** before starting significant work to ensure alignment.
2.  **Follow Conventions:** Ensure all new models use the `Trendyol` prefix.
3.  **Tests:** Run `dotnet test` (it runs on net8.0, net9.0 and net10.0). The suite calls every client method against a fake handler and checks the models against Trendyol's documented example payloads; add a fixture when you add a model.

## Disclaimer

This library is an unofficial open-source project and is **not** affiliated with, endorsed by, or supported by Trendyol Ticaret A.Ş.

- The software is provided "as is", without warranty of any kind.
- You are responsible for managing API limits and ensuring data validity.
- For official support, please refer to the [Trendyol Developer Portal](https://developers.trendyol.com/).

## License

[MIT License](https://www.google.com/search?q=LICENSE)
