namespace TeaShop.Domain.InventoryQuery;


/// <summary>
///     Filters inventory items by stock availability.
/// </summary>
public class AvailabilityFilterDecorator(IInventoryQuery inner, bool isAvailable) : InventoryQueryDecoratorBase(inner)
{
    private readonly bool _isAvailable = isAvailable;


    public override IReadOnlyList<QueriedInventoryItem> Execute()
    {
        var availabilityResults = _inner.Execute();
        return availabilityResults.Where(item =>
            item.Item.Quantity > 0 == _isAvailable).ToList();
    }
}