using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;

/// <summary>
///     Bundles query results with the list of applied filters and sorts for display.
/// </summary>
public sealed class InventoryQueryOutput(IReadOnlyList<QueriedInventoryItem> items, IReadOnlyList<FilterDescription> appliedFilters)
{
    public IReadOnlyList<QueriedInventoryItem> Items { get; } = items ?? throw new ArgumentNullException(nameof(items));

    public IReadOnlyList<FilterDescription> AppliedFiltersAndSorts { get; } =
        appliedFilters ?? throw new ArgumentNullException(nameof(appliedFilters));
}