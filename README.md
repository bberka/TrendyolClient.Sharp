

<p align="center">
  <img src="https://raw.githubusercontent.com/bberka/TrendyolClient.Sharp/master/assets/icon.png" width="128" height="128" alt="TrendyolClient.Sharp icon">
</p>

# TrendyolClient.Sharp

[![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-512BD4)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Nuget](https://img.shields.io/badge/nuget-TrendyolClient.Sharp-blue)](https://www.nuget.org/packages/TrendyolClient.Sharp)

A strongly-typed, community-driven C\# client library for the [Trendyol API](https://developers.trendyol.com/). Built on **Refit**, it wraps the Marketplace, International Marketplace, Finance, Webhook, Global Partner, and Export Center endpoints and creates clients lazily to keep memory use low.

> **Status: 2.x release line**
>
> The client follows Trendyol's current (V2) API documentation, and its automated tests check routes, request and response shapes, and error handling. It has not been verified against the live Trendyol API for every endpoint, so some calls may behave differently than documented. Try the calls you depend on against Trendyol's stage environment first, and please [open an issue](https://github.com/bberka/TrendyolClient.Sharp/issues) when something does not match.
>
> The library does not validate input, throttle or retry (see [Scope](#scope)).

## Features

- **Modular Design:** Separate clients for Marketplace, International Marketplace, Finance, Webhook, Global Partner and Export Center (AutoFT) APIs.
- **Lazy Loading:** Uses `Lazy<T>` to instantiate specific clients only when accessed, reducing memory footprint.
- **Type Safety:** Full C\# strong typing for requests, responses, and enumerations.
- **Typed errors:** Optional mapping of Trendyol HTTP status codes to specific C\# exceptions (e.g., `TrendyolRateLimitException`); see [Error Handling](#error-handling).
- **DI Ready:** Registers through ASP.NET Core `IServiceCollection`.
- **Multi-Environment:** Native support for switching between Production and Staging APIs.

## Scope

This package does one thing: it gives you **type-safe access to the Trendyol API**, following Trendyol's documentation and .NET best practices (typed requests and responses, `IHttpClientFactory`, dependency injection, Refit). It is a thin, faithful client, not a framework around the API.

**What it does not do, and never will:**

- **No input validation.** It sends what you give it. Trendyol validates requests and reports problems in the response; check the response (or enable `ThrowOnError`) to see them.
- **No rate-limit checks.** Trendyol allows 50 requests per 10 seconds per endpoint. Staying under that is your responsibility; the library does not throttle, queue or count requests.
- **No retries.** Failed calls are not repeated. Many calls are not safe to repeat (creating products, splitting packages, creating claims), and the right policy depends on your workload.
- **No business logic.** No caching of Trendyol data, no mapping to your domain model, no synchronization jobs.

Keeping these out keeps the library small, predictable and easy to keep in step with Trendyol's API. If you need them, add them around the client: see [Retries, throttling and other middleware](#retries-throttling-and-other-middleware).

## Supported frameworks

The package targets **.NET 8**, **.NET 9** and **.NET 10** (`net8.0`, `net9.0`, `net10.0`). Building from source needs the **.NET 10 SDK** (pinned in `global.json`); running the tests on all three targets needs the .NET 8, 9 and 10 runtimes.

## Installation

```bash
dotnet add package TrendyolClient.Sharp
```

## Quick Start

### 1\. Registration

```csharp
builder.Services.AddTrendyolApiClient(config =>
{
    config.IntegrationName = "MyCompanyIntegration"; // see "User-Agent" below
    config.EnableLogging = true;
    config.RequestTimeoutSeconds = 60;
    config.ThrowOnError = true; // optional: throw typed exceptions, see "Error Handling"
});
```

`AddTrendyolApiClient` returns the `IHttpClientBuilder` of the `HttpClient` all clients use, so you can chain your own handlers (see [Retries, throttling and other middleware](#retries-throttling-and-other-middleware)).

### 2\. Get a client

Pick the setup that fits how many sellers you serve.

For one seller, register the credentials once and inject the client. The clients are singletons, and each area interface (`ITrendyolOrderApi`, `ITrendyolProductApi`, `ITrendyolClaimApi`, ...) is registered too, so a class can depend on, and a test can fake, just the area it uses.

```csharp
builder.Services.AddTrendyolApiClient();
builder.Services.AddTrendyolSeller(new TrendyolCredentials(sellerId, apiKey, apiSecret));

// or read them when first needed, from configuration or a secret store:
builder.Services.AddTrendyolSeller(sp =>
    sp.GetRequiredService<IOptions<MyTrendyolOptions>>().Value.ToCredentials());

public class InventoryService(ITrendyolMarketplaceApi trendyol) { /* ... */ }
```

For several sellers, or credentials that change, use the factory. Pass a `TrendyolCredentials` (or the same values as separate arguments); clients are cached per seller, environment and storefront, and a new key or secret rebuilds the client.

```csharp
public class SyncJob(TrendyolMarketplaceClientFactory factory)
{
    public async Task Run(TrendyolCredentials credentials)
    {
        var client = factory.GetOrCreateMarketplaceClient(credentials);
        // ...
    }
}
```

`TrendyolCredentials` leaves the key and secret out of `ToString()`, so it is safe in a log line. Clients: Marketplace, International, Webhook, Finance, Global Partner and Export (see [International Marketplace](#international-marketplace) for the other regions).

### 3\. Make a call

```csharp
public class ProductService(ITrendyolMarketplaceApi client)
{
    public async Task SyncInventory()
    {
        var response = await client.UpdatePriceAndInventoryAsync(new TrendyolRequestUpdatePriceAndInventory
        {
            Items = new List<TrendyolPriceAndInventoryItem>
            {
                new() { Barcode = "123456789", Quantity = 50, SalePrice = 199.90m, ListPrice = 249.90m }
            }
        });

        // Every method returns Refit's IApiResponse<T>. Product writes are asynchronous: they answer 200 when queued,
        // and Trendyol checks the items afterwards. Read the outcome with the batch id.
        var batch = await client.GetBatchRequestResultAsync(response.Content.BatchRequestId);
        batch.Content.EnsureSuccess(); // throws TrendyolBatchRequestFailedException if any item was rejected
    }
}
```

### Batch results

Product operations (create, update, price and inventory, delete, archive, unlock) return a `batchRequestId` as soon as Trendyol accepts them. A rejected barcode or a missing brand only shows up later in `GetBatchRequestResultAsync`. The result has helpers so a failure is hard to miss:

| Member | Meaning |
| :----- | :------ |
| `IsCompleted` | Trendyol finished the batch (status `COMPLETED`; for stock and price updates, which return no status, no item is `IN_PROGRESS`). |
| `HasFailures` / `FailedItems` | Whether items were rejected, and which, with `FailureReasons`. |
| `EnsureSuccess()` | Throws `TrendyolBatchRequestFailedException` (batch id, failed count, failed items) if anything failed; returns the result otherwise. |

The library does not poll: how long to wait before reading the result is up to you.

## Architecture & Performance

This library uses a **Lazy-Loaded Container Pattern**.

When you call `GetOrCreateMarketplaceClient`, the factory:

1.  Checks an internal `ConcurrentDictionary` for an existing client set for that seller, environment (production or stage) and storefront.
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

### International client (`GetOrCreateInternationalClient`) and Global Partner client (`GetOrCreateGlobalPartnerClient`)

See [International Marketplace](#international-marketplace) and [Global Partner](#global-partner) below.

## International Marketplace

Trendyol's International Marketplace (Germany, CEE and GULF; the "International Marketplace" docs) uses the same hosts and mostly the same services as the Türkiye marketplace, selected by a **`storeFrontCode`** header: `DE`, `SA`, `AE`, `KW`, `GR`, `SK`, `RO`, `CZ`, `BG`. Without it Trendyol treats the request as Türkiye (`TR`).

The storefront is set **once when you create the client** and is sent on every request of that client, so a seller who sells in two countries uses two clients:

```csharp
// Shared services (products V2, price and inventory, order packages, webhooks, labels, ...):
var marketplace = _factory.GetOrCreateMarketplaceClient(sellerId, apiKey, apiSecret, storeFrontCode: "DE");

// Region specific services (returns per region, tracking details, EU/GULF test orders, country-aware webhooks):
var international = _factory.GetOrCreateInternationalClient(sellerId, apiKey, apiSecret, storeFrontCode: "DE");
```

| Region | Storefronts | Notes |
| :----- | :---------- | :---- |
| Germany | `DE` | Returns: `GetClaimsEuropeAsync`, `CreateClaimAsAcceptedEuropeAsync`. |
| CEE | `GR`, `SK`, `RO`, `CZ`, `BG` | Europe return methods (`...EuropeAsync`); box dimensions in `NotifyIntlBoxInfoAsync`; `sgrPrice` and SGR fees are Romania only. |
| GULF | `SA`, `AE`, `KW` | GULF return methods (`...GulfAsync`, dedicated `-gulf` paths); `shipmentAddressId`, `returningAddressId` and `cargoProviders` apply to `SA` and `AE`. |

What is where:

- **Marketplace client** (with a `storeFrontCode`): everything the Türkiye docs and the International docs share, e.g. brands, categories, product create and updates (V2), price and inventory, batch results, order packages (`/v2/orders` and the stream), package status, cargo provider changes, alternative delivery, manual deliver/return, common labels.
- **Webhook client** (`GetOrCreateWebhookClient(..., storeFrontCode: "DE")`): webhook update, delete, activate and deactivate. Creating and listing webhooks differs for International (`countryCodes`), so those two are on the International client.
- **International client**: only what differs for International: `UpdateIntlTrackingNumberAsync` (`tracking-details`; needed for Seller Pays packages), cancel and split variants with `shouldKeepPreviousStatus`, box info with dimensions, EU and GULF test orders, webhooks with `countryCodes`, and the regional return flows (get, create, approve, reject with reason, audits, rejected package tracking number).
- The International **V1** product and order services were shut down on 15 October 2026 like the Türkiye ones, so they are not included.
- Rate limits are shared per group (product read, product write, price and inventory write), plus 30 price updates per minute per barcode.

## Global Partner

The Global Partner (Global Center Gateway) API lives under `/integration/exportcentergw/v1` on the same hosts: offers, offer content medias, stocks, order creation and cancellation, and the vendorRef/B2B lookups. It authenticates with Basic auth like the other APIs, has no seller ID in its routes, and needs the `x-correlationid` (a UUID per request) and `x-agentname` headers, which are method parameters. Paging for offers uses `size` and `pageKey` in the response body. No rate limits are documented.

```csharp
var global = _factory.GetOrCreateGlobalPartnerClient(sellerId, apiKey, apiSecret);
var stocks = await global.GetStocksAsync(new TrendyolGlobalFilterGetStocks { /* ... */ }, correlationId: Guid.NewGuid().ToString(), agentName: "Acme");
```

## Migrating from the 0.x nightly builds

Before the first stable release the package was only published as `0.0.1-nightly.*` builds. Trendyol closed the **Product V1** and **Order V1** services on 15 October 2026 (Türkiye and International), so the matching methods were removed rather than kept.

| Removed (0.x nightlies) | Use instead |
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
| no response | `TrendyolNetworkException` | The connection failed or the request timed out (`InnerException` has the cause). A call you cancelled yourself is not wrapped. |
| other | `TrendyolRequestFailedException` | For example `405`, `414`, `415`. Carries `StatusCode` and `ResponseBody`. |

Trendyol allows **50 requests per 10 seconds per endpoint**; the 51st gets a `429`.

## Retries, throttling and other middleware

The library does not retry, throttle or validate (see [Scope](#scope)). When you need it, add it to the `HttpClient` pipeline through the builder `AddTrendyolApiClient` returns. A handler runs for every call of every Trendyol client, and it can read the seller from the `X-Internal-Seller-Id` request header (the header is removed just before the request leaves, so Trendyol never sees it).

```csharp
builder.Services.AddTrendyolApiClient(config => config.ThrowOnError = false)
    .AddHttpMessageHandler<MyThrottlingHandler>();   // your own DelegatingHandler
```

With `Microsoft.Extensions.Http.Resilience`, retry only what is safe to repeat: reads, and the `429` that Trendyol returns when you exceed its limit. Do not blindly retry writes: creating products, splitting packages and creating claims are not idempotent.

```csharp
builder.Services.AddTrendyolApiClient()
    .AddResilienceHandler("trendyol", pipeline =>
    {
        pipeline.AddRetry(new HttpRetryStrategyOptions
        {
            MaxRetryAttempts = 3,
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
            ShouldHandle = args => ValueTask.FromResult(
                args.Outcome.Result?.StatusCode == HttpStatusCode.TooManyRequests ||
                (args.Outcome.Result?.RequestMessage?.Method == HttpMethod.Get &&
                 args.Outcome.Result.StatusCode >= HttpStatusCode.InternalServerError))
        });
    });
```

Trendyol allows 50 requests per 10 seconds per endpoint. To stay under it, put a rate limiter in the same pipeline (`AddRateLimiter`, or your own handler keyed on the seller header).

## Advanced Configuration

### Using the staging environment

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

## Releasing

The **`VERSION`** file in the repository root holds only `MAJOR.MINOR` (for example `2.0`). The **patch number is computed**, never committed: it is the highest existing `v<MAJOR>.<MINOR>.*` release tag plus one, or `0` if that line has no release yet. So with `VERSION` = `2.0`, the first release is `2.0.0`, the next `2.0.1`, and so on. Change `VERSION` only for a new minor (`2.1`) or major (`3.0`) version; the patch restarts at `0`. The calculation lives in `.github/scripts/next-version.sh`.

| Workflow | Trigger | What it does |
| :------- | :------ | :----------- |
| `ci.yml` | every pull request | Builds, runs the tests on net8.0, net9.0 and net10.0, and packs. Mark the **Build & test** check as required in the branch protection rules so a pull request cannot merge unless it passes. |
| `nightly-release.yml` | push to `master` | Runs the tests, then publishes `<next version>-nightly.<timestamp>` (for example `2.0.1-nightly.202611121200`) to NuGet as a pre-release. |
| `release.yml` | manual (**Actions > Release > Run workflow**, on `master`) | Runs the tests, publishes the next version (for example `2.0.1`) to NuGet, then creates the `v2.0.1` tag and GitHub release. Tick *dry run* to build, test and pack without publishing. |

There are no release candidates: every merge to `master` produces a nightly, and a stable version is published only when you run **Release**. The release workflow refuses to run outside `master`, and it never silently skips a version that already exists on NuGet.

### One-time NuGet setup (trusted publishing)

The workflows publish with [NuGet trusted publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing), so **no NuGet API key is stored in the repository**. GitHub proves which workflow is running, and nuget.org hands it a temporary API key that is valid for one hour. Set it up once:

1. On nuget.org: click your username, then **Trusted Publishing**, and add one policy per publishing workflow:

   | Repository Owner | Repository | Workflow File | Environment |
   | :--------------- | :--------- | :------------ | :---------- |
   | `bberka` | `TrendyolClient.Sharp` | `nightly-release.yml` | (leave empty) |
   | `bberka` | `TrendyolClient.Sharp` | `release.yml` | (leave empty) |

   The workflow file is the file name only, without `.github/workflows/`.
2. On GitHub: **Settings > Secrets and variables > Actions > Variables**, add a repository variable `NUGET_USER` with your nuget.org **username** (the profile name, not your email address).
3. Delete the old `NUGET_API_KEY` secret and revoke that key on nuget.org.

The first stable release will be **2.0.0**, matching Trendyol's Product V2 and Order V2 services. There is no 1.x: before this the package was only published as `0.0.1-nightly.*` builds.

A local `dotnet build` or `dotnet pack` produces `<MAJOR.MINOR>.0-local`, so a local package can never be mistaken for a release.

## Contributing

Open an issue before significant work, so we agree on the direction first.

### Setup

Install the .NET 10 SDK (and the .NET 8 and 9 runtimes to run every test target), clone the repository, and run `dotnet restore`. The first restore installs Git hooks with [Husky.Net](https://alirezanet.github.io/Husky.Net/):

| Hook | What it checks |
| :--- | :------------- |
| `pre-commit` | Builds with warnings as errors and runs the tests, unless only docs changed. Checks that Markdown code fences are closed. |
| `commit-msg` | Subject of at most 72 characters without a trailing period, a blank second line, body wrapped at 72. |

Run the build and test check by hand with `bash scripts/check-dotnet.sh`. Set `HUSKY=0` to skip installing the hooks, and do not bypass them with `--no-verify`.

### Pull requests

Branch from `master` (a separate Git worktree per task keeps work apart), commit in logical groups, and open the pull request when the work is done. `master` is protected: the **Build & test** check must pass before a merge.

- **Conventions:** models are one class per file with the `Trendyol` prefix (`TrendyolIntl` and `TrendyolGlobal` for those clients).
- **Tests:** run `dotnet test`. The suite calls every client method against a fake handler and checks the models against Trendyol's documented example payloads. Add a fixture when you add a model.
- **Docs:** update the README in the same pull request.

AI coding agents follow [AGENTS.md](AGENTS.md), which describes the workflow in full.

## Disclaimer

This library is an unofficial open-source project and is **not** affiliated with, endorsed by, or supported by Trendyol Ticaret A.Ş.

- The software is provided "as is", without warranty of any kind.
- You are responsible for managing API limits and ensuring data validity.
- For official support, please refer to the [Trendyol Developer Portal](https://developers.trendyol.com/).

## License

[MIT License](https://www.google.com/search?q=LICENSE)
