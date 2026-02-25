using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Returns the full inventory with no filters applied.
///     Serves as the base query in the decorator chain.
/// </summary>
public sealed class AllInventoryQuery(InventoryRepository stock) : IInventoryQuery
{
    private readonly InventoryRepository _stock = stock ?? throw new ArgumentNullException(nameof(stock));


    public IReadOnlyList<FilterDescription> AppliedFiltersAndSorts { get; } = [];

    public IReadOnlyList<QueriedInventoryItem> Execute()
    {
        return
            _stock.Items.Select((item, index) =>
                new QueriedInventoryItem(index + 1, item)).ToList();
    }
}