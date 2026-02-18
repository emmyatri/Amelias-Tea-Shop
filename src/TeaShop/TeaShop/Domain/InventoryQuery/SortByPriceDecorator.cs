namespace TeaShop.Domain.InventoryQuery;

public class SortByPriceDecorator : InventoryQueryDecoratorBase
{
    private readonly SortDirection _priceDirection;


    public SortByPriceDecorator(IInventoryQuery inner, SortDirection priceDirection) : base(inner)
    {
        _priceDirection = priceDirection;
    }

    public override List<QueriedInventoryItem> Execute()

    {
        var searchDirectionResults = _inner.Execute();

        if (_priceDirection == SortDirection.Ascending)
        {
            return searchDirectionResults.OrderBy(item =>
                item.Item.Price).ToList();
        }

        else
        {
            return searchDirectionResults.OrderByDescending(item =>
                item.Item.Price).ToList();
        }
    }
}