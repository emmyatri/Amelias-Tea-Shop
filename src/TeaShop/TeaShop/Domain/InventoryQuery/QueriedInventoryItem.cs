using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;


/// <summary>
///     Wraps an <see cref="InventoryItem"/> as it passes through the query pipeline.
/// </summary>
public class QueriedInventoryItem(InventoryItem item)
{
    public InventoryItem Item { get; } = item ??  throw new ArgumentNullException(nameof(item));
}