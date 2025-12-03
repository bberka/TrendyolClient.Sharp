namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when a requested resource is not found
/// </summary>
internal class TrendyolNotFoundException(
    string message,
    string resourceType,
    string resourceId) : TrendyolException(message)
{
    public TrendyolNotFoundException(string resourceType, string resourceId) : this($"{resourceType} with ID '{resourceId}' was not found", resourceType, resourceId)
    {
    }

    /// <summary>
    ///     Type of resource that was not found (e.g., "Product", "Order", "Webhook")
    /// </summary>
    public string ResourceType { get; } = resourceType;

    /// <summary>
    ///     Identifier of the resource that was not found
    /// </summary>
    public string ResourceId { get; } = resourceId;

    public override string ToString()
    {
        return $"{Message} [Type: {ResourceType}, ID: {ResourceId}]";
    }
}