

# TrendyolClient.Sharp

[![.NET](https://img.shields.io/badge/.NET-8.0%2B-512BD4)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Nuget](https://img.shields.io/badge/nuget-TrendyolClient.Sharp-blue)](https://www.nuget.org/packages/TrendyolClient.Sharp)

A modern, strongly-typed, and community-driven C\# client library for the [Trendyol API](https://developers.trendyol.com/). Built on top of **Refit**, this library provides a robust wrapper for Marketplace, Finance, and Webhook endpoints with a focus on memory efficiency and developer experience.

> **⚠️ PROJECT STATUS: ACTIVE DEVELOPMENT**
>
> This project is actively maintained for bug fixes and issue resolution; however, it is **not yet considered production-ready**.
>
> - **Breaking Changes:** The public API may change significantly between versions.
> - **Validation:** Input validation is minimal; you must validate data before sending it.
> - **Use at your own risk.**

## Features

- **Modular Design:** Separate clients for Marketplace, Finance, and Webhook APIs.
- **Lazy Loading:** Uses `Lazy<T>` to instantiate specific clients only when accessed, reducing memory footprint.
- **Type Safety:** Full C\# strong typing for requests, responses, and enumerations.
- **Resilience:** Built-in mapping of Trendyol HTTP status codes to specific C\# exceptions (e.g., `TrendyolRateLimitException`).
- **DI Ready:** Seamless integration with ASP.NET Core `IServiceCollection`.
- **Multi-Environment:** Native support for switching between Production and Staging APIs.

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
    config.IntegrationName = "MyCompanyIntegration";
    config.EnableLogging = true;
    config.RequestTimeoutSeconds = 60;
});
```

### 2\. Usage

Inject `TrendyolMarketplaceClientFactory` into your service. You can request specific clients (Marketplace, Webhook, or Finance) based on your needs.

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

        // 2. Make the API call
        try
        {
            var response = await client.UpdateInventoryAsync(new TrendyolRequestUpdateInventory
            {
                Items = new List<InventoryItem>
                {
                    new InventoryItem { Barcode = "123456789", Quantity = 50 }
                }
            });
        }
        catch (TrendyolRateLimitException ex)
        {
            // Handle HTTP 429 specifically
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

The `ClientContainer` initializes a single `HttpClient` shared across all contexts. However, the Refit proxies for Marketplace, Finance, or Webhook are **only created when you access them**. If you only use the Finance API, the memory overhead for the Marketplace and Webhook definitions is never allocated.

## API Coverage

### Marketplace API

- **Products:** Create, update price/inventory, batch operations.
- **Orders:** Fetch packages, update status (shipped, delivered), split packages.
- **Claims:** Manage returns and claims.
- **Locations:** Fetch addresses, cities, and districts.

### Finance API

- **Settlements:** Retrieve settlement details and transaction logs.
- **Invoices:** Access cargo invoice data.

### Webhook API

- **Management:** Create, update, and verify webhooks for real-time updates.

## Error Handling

The library abstracts HTTP status codes into typed exceptions for cleaner control flow.

| HTTP Code | Exception Type                    | Description                                              |
| :-------- | :-------------------------------- | :------------------------------------------------------- |
| `401`     | `TrendyolAuthenticationException` | Invalid API Key/Secret.                                  |
| `403`     | `TrendyolAuthenticationException` | Forbidden (Permissions issue).                           |
| `400`     | `TrendyolValidationException`     | Invalid request parameters (includes API error message). |
| `404`     | `TrendyolNotFoundException`       | Resource not found.                                      |
| `429`     | `TrendyolRateLimitException`      | Too many requests. Contains `RetryAfter` property.       |
| `422`     | `TrendyolBusinessRuleException`   | Unprocessable Entity (Business logic violation).         |
| `500+`    | `TrendyolServerException`         | Trendyol internal platform error.                        |

## Advanced Configuration

### Using the Staging Environment

You can test your integration against Trendyol's staging environment by setting the `useStageApi` flag to `true`.

```csharp
var client = _factory.GetOrCreateMarketplaceClient(
    sellerId,
    apiKey,
    apiSecret,
    useStageApi: true // Points to stageapigw.trendyol.com
);
```

### Manual Cache Invalidation

If a seller regenerates their API credentials, you must invalidate the cached client to force a reconstruction.

```csharp
_factory.InvalidateClient(sellerId);
```

## Contributing

We welcome contributions\! Since this is a community project:

1.  **Open an Issue** before starting significant work to ensure alignment.
2.  **Follow Conventions:** Ensure all new models use the `Trendyol` prefix.
3.  **Tests:** While currently limited, please attempt to add unit tests for new logic.

## Disclaimer

This library is an unofficial open-source project and is **not** affiliated with, endorsed by, or supported by Trendyol Ticaret A.Ş.

- The software is provided "as is", without warranty of any kind.
- You are responsible for managing API limits and ensuring data validity.
- For official support, please refer to the [Trendyol Developer Portal](https://developers.trendyol.com/).

## License

[MIT License](https://www.google.com/search?q=LICENSE)
