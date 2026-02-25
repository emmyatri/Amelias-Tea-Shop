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
    
    public override IReadOnlyList<QueriedInventoryItem> Execute()

    {
        var searchDirectionResults = _inner.Execute();

        if (_priceDirection == SortDirection.Ascending)
            return searchDirectionResults.OrderBy(item =>
                item.Item.Price).ToList();

        return searchDirectionResults.OrderByDescending(item =>
            item.Item.Price).ToList();
    }
}