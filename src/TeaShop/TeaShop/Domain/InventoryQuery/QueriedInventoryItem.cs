using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

public class QueriedInventoryItem(InventoryItem item)
{
    public InventoryItem Item { get; } = item ??  throw new ArgumentNullException(nameof(item));
}