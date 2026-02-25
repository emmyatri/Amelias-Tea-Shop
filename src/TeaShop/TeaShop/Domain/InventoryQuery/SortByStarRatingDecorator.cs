namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Sorts inventory items by star rating in the specified direction.
/// </summary>
public class SortByStarRatingDecorator(IInventoryQuery inner, SortDirection starDirection)
    : InventoryQueryDecoratorBase(inner)
{
    private readonly SortDirection _starDirection = starDirection;


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