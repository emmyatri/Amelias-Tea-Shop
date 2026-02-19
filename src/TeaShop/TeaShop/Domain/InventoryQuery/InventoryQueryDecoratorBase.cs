namespace TeaShop.Domain.InventoryQuery;

public abstract class InventoryQueryDecoratorBase(IInventoryQuery inner) : IInventoryQuery 
{
    protected readonly IInventoryQuery _inner = inner;


    public abstract List<QueriedInventoryItem> Execute();
}