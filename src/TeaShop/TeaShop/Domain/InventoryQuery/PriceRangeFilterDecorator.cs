namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Filters inventory items within a minimum and maximum price range (inclusive).
/// </summary>
public class PriceRangeFilterDecorator(IInventoryQuery inner, decimal searchPriceMin, decimal searchPriceMax)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly decimal _searchPriceMax = searchPriceMax;
    private readonly decimal _searchPriceMin = searchPriceMin;

    public override IReadOnlyList<QueriedInventoryItem> Execute()
    {
        var priceRangeResults = _inner.Execute();
        return priceRangeResults.Where(item =>
            item.Item.Price >= _searchPriceMin && item.Item.Price <= _searchPriceMax).ToList();
    }
}