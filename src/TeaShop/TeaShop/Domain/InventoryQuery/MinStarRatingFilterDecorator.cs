using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Filters inventory items that meet or exceed a minimum star rating.
/// </summary>
public class MinStarRatingFilterDecorator(IInventoryQuery inner, StarRating searchStarRatingMin)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly StarRating _searchStarRatingMin =
        searchStarRatingMin ?? throw new ArgumentNullException(nameof(searchStarRatingMin));


    public override IReadOnlyList<QueriedInventoryItem> Execute()
    {
        var minStarRatingResults = _inner.Execute();
        return minStarRatingResults.Where(item =>
            item.Item.StarRating.CompareTo(_searchStarRatingMin) >= 0).ToList();
    }
}