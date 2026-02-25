using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;

/// <summary>
///     Bundles query results with the list of applied filters and sorts for display.
/// </summary>
public sealed class InventoryQueryOutput(IReadOnlyList<QueriedInventoryItem> items, IReadOnlyList<string> appliedFilters)
{
    public IReadOnlyList<QueriedInventoryItem> Items { get; } = items ?? throw new ArgumentNullException(nameof(items));

    public IReadOnlyList<string> AppliedFilter { get; } =
        appliedFilters ?? throw new ArgumentNullException(nameof(appliedFilters));
}