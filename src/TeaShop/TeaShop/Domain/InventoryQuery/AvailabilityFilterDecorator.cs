namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Filters inventory items by stock availability.
/// </summary>
public sealed class AvailabilityFilterDecorator(IInventoryQuery inner, bool isAvailable) : InventoryQueryDecoratorBase(inner)
{
    private readonly bool _isAvailable = isAvailable;

    protected override FilterDescription? AppliedDescription
        => new("Filter", _isAvailable
            ? "Availability = In Stock (Quantity > 0)"
            : "Availability = Out of Stock (Quantity = 0)");

    protected override IReadOnlyList<QueriedInventoryItem> Decorate(IReadOnlyList<QueriedInventoryItem> items)
    {
        return items.Where(item => item.Quantity > 0 == _isAvailable).ToList();
    }
}