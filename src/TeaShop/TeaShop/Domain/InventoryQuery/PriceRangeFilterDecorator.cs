namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Filters inventory items within a minimum and maximum price range (inclusive).
/// </summary>
public sealed class PriceRangeFilterDecorator(IInventoryQuery inner, decimal searchPriceMin, decimal searchPriceMax)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly decimal _searchPriceMax = searchPriceMax;
    private readonly decimal _searchPriceMin = searchPriceMin;

    protected override FilterDescription? AppliedDescription
        => new("Filter", $"Price range between {_searchPriceMin:C} and {_searchPriceMax:C}");

    protected override IReadOnlyList<QueriedInventoryItem> Decorate(IReadOnlyList<QueriedInventoryItem> items)
    {
        return items.Where(item =>
            item.Price >= _searchPriceMin && item.Price <= _searchPriceMax).ToList();
    }
}