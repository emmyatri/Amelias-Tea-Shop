namespace TeaShop.Domain.InventoryQuery;

public abstract class InventoryQueryDecoratorBase(IInventoryQuery inner) : IInventoryQuery
{
    protected readonly IInventoryQuery _inner = inner ?? throw new ArgumentNullException(nameof(inner));


    public abstract IReadOnlyList<QueriedInventoryItem> Execute();
}