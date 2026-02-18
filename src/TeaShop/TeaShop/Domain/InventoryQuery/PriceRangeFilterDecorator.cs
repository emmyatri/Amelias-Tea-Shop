namespace TeaShop.Domain.InventoryQuery;

public class PriceRangeFilterDecorator : InventoryQueryDecoratorBase
{
    private readonly decimal _searchPriceMin;
    private readonly decimal _searchPriceMax;

    public PriceRangeFilterDecorator(IInventoryQuery inner, decimal searchPriceMin, decimal searchPriceMax) : base(inner)
    {
        _searchPriceMin = searchPriceMin;
        _searchPriceMax = searchPriceMax;
    }

    public override List<QueriedInventoryItem> Execute()
    {
        var priceRangeResults = _inner.Execute();
        return priceRangeResults.Where(item =>
            (item.Item.Price >= _searchPriceMin && item.Item.Price <= _searchPriceMax)).ToList();
    }
    
    
}