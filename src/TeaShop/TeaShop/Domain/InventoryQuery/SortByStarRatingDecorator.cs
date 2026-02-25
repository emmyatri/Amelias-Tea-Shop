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

    public override IReadOnlyList<QueriedInventoryItem> Execute()

    {
        var searchDirectionResults = _inner.Execute();

        if (_starDirection == SortDirection.Ascending)
            return searchDirectionResults.OrderBy(item =>
                item.Item.StarRating).ToList();

        return searchDirectionResults.OrderByDescending(item =>
            item.Item.StarRating).ToList();
    }
}