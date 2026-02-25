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
    IReadOnlyList<QueriedInventoryItem> Execute();
}