using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Wraps an <see cref="InventoryItem"/> as it passes through the query pipeline.
/// </summary>
public record QueriedInventoryItem(
    int Index,
    Guid Id,
    string Name,
    decimal Price,
    int Quantity,
    StarRating StarRating
) : InventoryItem(Id, Name, Price, Quantity, StarRating)

{
    public QueriedInventoryItem(int index, InventoryItem item)
        : this(index, item.Id, item.Name, item.Price, item.Quantity, item.StarRating)
    {
    }
}