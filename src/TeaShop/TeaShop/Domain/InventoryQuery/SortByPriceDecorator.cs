namespace TeaShop.Domain.InventoryQuery;

public class SortByPriceDecorator(IInventoryQuery inner, SortDirection priceDirection): InventoryQueryDecoratorBase (inner)
{
    private readonly SortDirection _priceDirection = priceDirection;
    
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