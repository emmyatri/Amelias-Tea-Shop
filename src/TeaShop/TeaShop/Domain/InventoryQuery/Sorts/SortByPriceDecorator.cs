using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery.Sorts;

/// <summary>
///     Sorts inventory items by price in the specified direction.
/// </summary>
public sealed class SortByPriceDecorator(IInventoryQuery inner, SortDirection priceDirection)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly SortDirection _priceDirection = priceDirection;

    protected override FilterDescription? AppliedDescription
        => new("Sort", $"Price ({_priceDirection.ToString().ToLower()})");

    protected override IReadOnlyList<InventoryItem> Decorate(IReadOnlyList<InventoryItem> items)
    {
        if (_priceDirection == SortDirection.Ascending)
            return items.OrderBy(item => item.Price).ToList().AsReadOnly();
        
        return  items.OrderByDescending(item => item.Price).ToList().AsReadOnly();
    }
}