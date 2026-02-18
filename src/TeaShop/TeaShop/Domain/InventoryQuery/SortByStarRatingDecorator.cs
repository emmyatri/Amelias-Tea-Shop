namespace TeaShop.Domain.InventoryQuery;

public class SortByStarRatingDecorator : InventoryQueryDecoratorBase
{
    private readonly SortDirection _starDirection;

    public SortByStarRatingDecorator(IInventoryQuery inner, SortDirection starDirection) : base(inner)
    {
        _starDirection = starDirection;
    }

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