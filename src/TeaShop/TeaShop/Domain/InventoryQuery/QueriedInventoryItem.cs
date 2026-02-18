using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

public class QueriedInventoryItem(InventoryItem item, int index)
{
    public InventoryItem Item { get; } = item;
    public int Index { get; } = index;

}