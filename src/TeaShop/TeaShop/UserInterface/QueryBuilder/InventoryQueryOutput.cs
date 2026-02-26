using TeaShop.Domain.Inventory;
using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;

public sealed class InventoryQueryOutput(
    IReadOnlyList<QueriedInventoryItem> items,
    IReadOnlyList<string> appliedFilters)
{
    public IReadOnlyList<QueriedInventoryItem> Items { get; } = items ?? throw new ArgumentNullException(nameof(items));
    public IReadOnlyList<string> AppliedFilters { get; } = 
        appliedFilters ?? throw new ArgumentNullException(nameof(appliedFilters));
    
    public static InventoryQueryOutput From(IInventoryQuery query)
    {
        ArgumentNullException.ThrowIfNull(query);
        
        var indexedItems = query.Execute()
            .Select((item, index) => new QueriedInventoryItem(index + 1, item))
            .ToList();

        return new InventoryQueryOutput(indexedItems, query.AppliedFiltersAndSorts);
    }
}