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

    protected override IReadOnlyList<QueriedInventoryItem> Decorate(IReadOnlyList<QueriedInventoryItem> items)
    {
        if (_starDirection == SortDirection.Ascending)
            return items.OrderBy(item => item.Item.StarRating).ToList();
        
        return  items.OrderByDescending(item => item.Item.StarRating).ToList();
    }
}