namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Sorts inventory items by price in the specified direction.
/// </summary>
public sealed class SortByPriceDecorator(IInventoryQuery inner, SortDirection priceDirection)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly SortDirection _priceDirection = priceDirection;

    protected override FilterDescription? AppliedDescription
        => new("Sort", $"Price ({_priceDirection.ToString().ToLower()})");

    protected override IReadOnlyList<QueriedInventoryItem> Decorate(IReadOnlyList<QueriedInventoryItem> items)
    {
        if (_priceDirection == SortDirection.Ascending)
            return items.OrderBy(item => item.Item.Price).ToList();
        
        return  items.OrderByDescending(item => item.Item.Price).ToList();
    }
}