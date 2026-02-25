using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;

public class InventoryQueryOutput(IReadOnlyList<QueriedInventoryItem> items, IReadOnlyList<string> appliedFilters)
{
    public IReadOnlyList<QueriedInventoryItem> Items { get; } = items ?? throw new ArgumentNullException(nameof(items));

    public IReadOnlyList<string> AppliedFilter { get; } =
        appliedFilters ?? throw new ArgumentNullException(nameof(appliedFilters));
}