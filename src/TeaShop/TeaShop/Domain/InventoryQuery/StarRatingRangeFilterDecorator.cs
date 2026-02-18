using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

public class StarRatingRangeFilterDecorator : InventoryQueryDecoratorBase
{
    private readonly StarRating _searchRangeMin;
    private readonly StarRating _searchRangeMax;

    public StarRatingRangeFilterDecorator(IInventoryQuery inner, StarRating searchRangeMin, StarRating searchRangeMax)
        : base(inner)
    {
        _searchRangeMin = searchRangeMin;
        _searchRangeMax = searchRangeMax;
    }

    public override List<QueriedInventoryItem> Execute()
    {
        var starRangeResults = _inner.Execute();
        return starRangeResults.Where(item =>
            (item.Item.StarRating.CompareTo(_searchRangeMin) >= 0 &&
             item.Item.StarRating.CompareTo(_searchRangeMax) <= 0)).ToList();
    }
}