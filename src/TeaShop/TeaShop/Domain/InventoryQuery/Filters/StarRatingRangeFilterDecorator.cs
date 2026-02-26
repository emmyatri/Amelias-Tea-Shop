using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery.Filters;

/// <summary>
///     Filters inventory items within a minimum and maximum star rating range (inclusive).
/// </summary>
public sealed class StarRatingRangeFilterDecorator : InventoryQueryDecoratorBase
{
    private readonly StarRating _searchRangeMax;

    private readonly StarRating _searchRangeMin;

    public StarRatingRangeFilterDecorator(IInventoryQuery inner, StarRating searchRangeMin, StarRating searchRangeMax)
        : base(inner)
    {
        _searchRangeMin = searchRangeMin ?? throw new ArgumentNullException(nameof(searchRangeMin));
        _searchRangeMax = searchRangeMax ?? throw new ArgumentNullException(nameof(searchRangeMax));

        if (searchRangeMin.StarValue > searchRangeMax.StarValue)
            throw new ArgumentException("Minimum star rating cannot exceed maximum star rating.");
    }
    
    protected override string? AppliedDescription
        => ($"Filter: Star rating between {_searchRangeMin.StarValue} and {_searchRangeMax.StarValue}");

    protected override IReadOnlyList<InventoryItem> Decorate(IReadOnlyList<InventoryItem> items)
    {
        return items.Where(item =>
            item.StarRating.CompareTo(_searchRangeMin) >= 0 &&
            item.StarRating.CompareTo(_searchRangeMax) <= 0).ToList().AsReadOnly();

    }
}