namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Describes a filter or sort applied by a decorator.
///     The domain provides structured data; the UI decides how to format it.
/// </summary>
public record FilterDescription(string Type, string Detail);