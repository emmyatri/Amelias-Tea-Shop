namespace TeaShop.Domain.InventoryQuery;

public class PriceRangeFilterDecorator(IInventoryQuery inner, decimal searchPriceMin, decimal searchPriceMax)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly decimal _searchPriceMax = searchPriceMax;
    private readonly decimal _searchPriceMin = searchPriceMin;

    public override List<QueriedInventoryItem> Execute()
    {
        var priceRangeResults = _inner.Execute();
        return priceRangeResults.Where(item =>
            item.Item.Price >= _searchPriceMin && item.Item.Price <= _searchPriceMax).ToList();
    }
}