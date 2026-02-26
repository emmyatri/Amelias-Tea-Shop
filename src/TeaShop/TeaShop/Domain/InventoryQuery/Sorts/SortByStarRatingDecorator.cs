using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Sorts inventory items by star rating in the specified direction.
/// </summary>
public sealed class SortByStarRatingDecorator(IInventoryQuery inner, SortDirection starDirection)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly SortDirection _starDirection = starDirection;

    protected override FilterDescription? AppliedDescription
        => new("Sort", $"Star Rating ({_starDirection.ToString().ToLower()})");

    protected override IReadOnlyList<InventoryItem> Decorate(IReadOnlyList<InventoryItem> items)
    {
        if (_starDirection == SortDirection.Ascending)
            return items.OrderBy(item => item.StarRating).ToList();
        
        return  items.OrderByDescending(item => item.StarRating).ToList();
    }
}