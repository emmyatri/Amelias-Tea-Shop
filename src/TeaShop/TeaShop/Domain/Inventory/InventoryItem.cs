namespace TeaShop.Domain.Inventory;

/// <summary>
///     Represents a single tea product in the inventory.
/// </summary>
public record InventoryItem(Guid Id, 
                            string Name, 
                            decimal Price, 
                            int Quantity, 
                            StarRating StarRating);