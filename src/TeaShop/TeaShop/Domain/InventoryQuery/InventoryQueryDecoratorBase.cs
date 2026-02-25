namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Base class for all query decorators. Wraps an inner query
///     and delegates execution to subclasses for filtering or sorting.
/// </summary>
public abstract class InventoryQueryDecoratorBase(IInventoryQuery inner) : IInventoryQuery
{
    protected readonly IInventoryQuery _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    protected virtual FilterDescription? AppliedDescription => null;

    public abstract IReadOnlyList<QueriedInventoryItem> Execute();

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