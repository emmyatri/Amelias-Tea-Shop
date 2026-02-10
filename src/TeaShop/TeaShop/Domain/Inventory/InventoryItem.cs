namespace TeaShop.Domain.Inventory;


/// <summary>
/// Represents a single tea product in the inventory.
/// </summary>
public class InventoryItem
{
    
    public Guid Id { get; }
    
    public string Name { get; }
    
    public decimal Price { get; }
    
    public int Quantity { get; private set; }
    
    public StarRating StarRating { get; }

    public InventoryItem(Guid id, string name, decimal price, int quantity, StarRating starRating)
    {
        
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        StarRating = starRating;
        
    }
    
    public void ReduceQuantity(int amount)
    {
        
        if (amount > Quantity)
            throw new InvalidOperationException("Not enough in stock.");
        
        Quantity -= amount;
        
    }
}