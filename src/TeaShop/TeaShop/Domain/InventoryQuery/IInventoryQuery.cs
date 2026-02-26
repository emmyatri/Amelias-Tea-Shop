using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Defines a composable inventory query. Implementations can be
///     chained using the Decorator pattern to build filter/sort pipelines.
/// </summary>
public interface IInventoryQuery
{
    /// <summary>
    ///     Executes the query and returns matching inventory items.
    /// </summary>
    IReadOnlyList<InventoryItem> Execute();

    /// <summary>
    ///     Gets the accumulated filter and sort descriptions from the query chain.
    /// </summary>
    IReadOnlyList<FilterDescription> AppliedFiltersAndSorts { get; }
}