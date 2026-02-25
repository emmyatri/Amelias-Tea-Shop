using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Filters inventory items within a minimum and maximum star rating range (inclusive).
/// </summary>
public sealed class StarRatingRangeFilterDecorator(IInventoryQuery inner, StarRating searchRangeMin, StarRating searchRangeMax)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly StarRating _searchRangeMax =
        searchRangeMax ?? throw new ArgumentNullException(nameof(searchRangeMax));

    private readonly StarRating _searchRangeMin =
        searchRangeMin ?? throw new ArgumentNullException(nameof(searchRangeMin));

    public override IReadOnlyList<QueriedInventoryItem> Execute()
    {
        var starRangeResults = _inner.Execute();
        return starRangeResults.Where(item =>
            item.Item.StarRating.CompareTo(_searchRangeMin) >= 0 &&
            item.Item.StarRating.CompareTo(_searchRangeMax) <= 0).ToList();
    }
}