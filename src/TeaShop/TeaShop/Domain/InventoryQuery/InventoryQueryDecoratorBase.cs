namespace TeaShop.Domain.InventoryQuery;

public abstract class InventoryQueryDecoratorBase : IInventoryQuery
{
    
    protected readonly IInventoryQuery _inner;
    

    protected InventoryQueryDecoratorBase(IInventoryQuery inner)
    {
        _inner = inner;
    }
    
    
    public abstract List<QueriedInventoryItem> Execute();
    
}