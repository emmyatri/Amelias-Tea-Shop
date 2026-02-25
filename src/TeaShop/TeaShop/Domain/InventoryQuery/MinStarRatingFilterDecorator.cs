using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Filters inventory items that meet or exceed a minimum star rating.
/// </summary>
public sealed class MinStarRatingFilterDecorator(IInventoryQuery inner, StarRating searchStarRatingMin)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly StarRating _searchStarRatingMin =
        searchStarRatingMin ?? throw new ArgumentNullException(nameof(searchStarRatingMin));

    protected override FilterDescription? AppliedDescription
        => new("Filter", $"Star rating >= {_searchStarRatingMin.StarValue}");

    
    protected override IReadOnlyList<QueriedInventoryItem> Decorate(IReadOnlyList<QueriedInventoryItem> items)
    {
        return items.Where(item =>
            item.Item.StarRating.CompareTo(_searchStarRatingMin) >= 0).ToList();
    }
}