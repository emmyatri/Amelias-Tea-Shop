namespace TeaShop.Domain.InventoryQuery;

public class AvailabilityFilterDecorator : InventoryQueryDecoratorBase
{
    private readonly bool _isAvailable;

    public AvailabilityFilterDecorator(IInventoryQuery inner, bool isAvailable) : base(inner)
    {
        _isAvailable = isAvailable;
    }

    public override List<QueriedInventoryItem> Execute()
    {
        var availabilityResults = _inner.Execute();
        return availabilityResults.Where(item => 
            (item.Item.Quantity >0) == _isAvailable).ToList();
    }
}