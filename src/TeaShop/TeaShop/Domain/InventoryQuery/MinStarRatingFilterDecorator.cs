using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

public class MinStarRatingFilterDecorator : InventoryQueryDecoratorBase
{
    private readonly StarRating _searchStarRatingMin;
    
    public MinStarRatingFilterDecorator(IInventoryQuery inner, StarRating searchStarRatingMin) : base(inner)
        {
        _searchStarRatingMin = searchStarRatingMin;
        }

    
    public override List<QueriedInventoryItem> Execute()
    {
        
        var minStarRatingResults = _inner.Execute();
        return minStarRatingResults.Where(item =>
            (item.Item.StarRating.CompareTo(_searchStarRatingMin) >= 0)).ToList();
        
    }
}