using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Base class for all query decorators. Wraps an inner query
///     and delegates execution to subclasses for filtering or sorting.
/// </summary>
public abstract class InventoryQueryDecoratorBase(IInventoryQuery inner) : IInventoryQuery
{
    protected readonly IInventoryQuery _inner = inner ?? throw new ArgumentNullException(nameof(inner));
   
    
    protected virtual FilterDescription? AppliedDescription => null;

    /// <summary>
    ///     Applies this decorator's specific filtering or sorting logic
    ///     to the results from the inner query.
    /// </summary>
    protected abstract IReadOnlyList<InventoryItem> Decorate(IReadOnlyList<InventoryItem> items);

    public IReadOnlyList<InventoryItem> Execute()
    {
        var items = _inner.Execute();
        return Decorate(items);
    }

    
    public IReadOnlyList<FilterDescription> AppliedFiltersAndSorts
    {
        get
        {
            if (AppliedDescription is null)
                return _inner.AppliedFiltersAndSorts;

            var combined = new List<FilterDescription>(_inner.AppliedFiltersAndSorts) { AppliedDescription };
            return combined.AsReadOnly();
        }
    }
}