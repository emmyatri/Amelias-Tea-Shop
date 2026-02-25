namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Base class for all query decorators. Wraps an inner query
///     and delegates execution to subclasses for filtering or sorting.
/// </summary>
public abstract class InventoryQueryDecoratorBase(IInventoryQuery inner) : IInventoryQuery
{
    protected readonly IInventoryQuery _inner = inner ?? throw new ArgumentNullException(nameof(inner));


    public abstract IReadOnlyList<QueriedInventoryItem> Execute();
}