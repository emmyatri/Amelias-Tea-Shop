using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

public class MinStarRatingFilterDecorator(IInventoryQuery inner, StarRating searchStarRatingMin) : InventoryQueryDecoratorBase (inner)
{
    private readonly StarRating _searchStarRatingMin = searchStarRatingMin;
 

    public override List<QueriedInventoryItem> Execute()
    {
        var minStarRatingResults = _inner.Execute();
        return minStarRatingResults.Where(item =>
            (item.Item.StarRating.CompareTo(_searchStarRatingMin) >= 0)).ToList();
    }
}