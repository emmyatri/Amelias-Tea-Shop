namespace TeaShop.Domain.Inventory;


/// <summary>
/// Represents a single tea product in the inventory.
/// </summary>
public class InventoryItem(Guid id, string name, decimal price, int quantity, StarRating starRating)
{

    public Guid Id { get; } = id;

    public string Name { get; } = name;

    public decimal Price { get; } = price;

    public int Quantity { get; private set; } = quantity;

    public StarRating StarRating { get; } = starRating;
    
    
    
    public void ReduceQuantity(int amount)
    {
        
        if (amount > Quantity)
            throw new InvalidOperationException("Not enough in stock.");
        
        Quantity -= amount;
        
    }

}
