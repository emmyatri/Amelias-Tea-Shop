namespace TeaShop.Domain.Inventory;

/// <summary>
///     Represents a single tea product in the inventory.
/// </summary>
public record InventoryItem(Guid Id, 
                            string Name, 
                            decimal Price, 
                            int Quantity, 
                            StarRating StarRating)

{
    public bool IsAvailable => Quantity > 0;

    public decimal PriceFor(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        return quantity * Price;
    }
    
}