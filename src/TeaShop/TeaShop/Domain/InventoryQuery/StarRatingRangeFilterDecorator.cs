using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

public class StarRatingRangeFilterDecorator(IInventoryQuery inner, StarRating searchRangeMin, StarRating searchRangeMax) : InventoryQueryDecoratorBase (inner)
{
    private readonly StarRating _searchRangeMin = searchRangeMin;
    private readonly StarRating _searchRangeMax = searchRangeMax;

    public override List<QueriedInventoryItem> Execute()
    {
        var starRangeResults = _inner.Execute();
        return starRangeResults.Where(item =>
            (item.Item.StarRating.CompareTo(_searchRangeMin) >= 0 &&
             item.Item.StarRating.CompareTo(_searchRangeMax) <= 0)).ToList();
    }
}