namespace TeaShop.Domain.InventoryQuery;

public class SortByStarRatingDecorator(IInventoryQuery inner, SortDirection starDirection) : InventoryQueryDecoratorBase (inner)
{
    private readonly SortDirection _starDirection = starDirection;
    

    public override List<QueriedInventoryItem> Execute()

    {
        var searchDirectionResults = _inner.Execute();

        if (_starDirection == SortDirection.Ascending)
        {
            return searchDirectionResults.OrderBy(item =>
                item.Item.StarRating).ToList();
        }
        else
        {
            return searchDirectionResults.OrderByDescending(item =>
                item.Item.StarRating).ToList();
        }
    }
}