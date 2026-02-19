namespace TeaShop.Domain.InventoryQuery;

public class AvailabilityFilterDecorator(IInventoryQuery inner, bool isAvailable) : InventoryQueryDecoratorBase (inner)
{
    private readonly bool _isAvailable = isAvailable;
    

    public override List<QueriedInventoryItem> Execute()
    {
        var availabilityResults = _inner.Execute();
        return availabilityResults.Where(item =>
            (item.Item.Quantity > 0) == _isAvailable).ToList();
    }
}