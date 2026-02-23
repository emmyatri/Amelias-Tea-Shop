namespace TeaShop.Domain.Payment;

public class PurchaseDetails(decimal price, string itemName, int quantity)
{
    public decimal Price { get; } = price;
    public string ItemName { get; } = itemName;
    public int Quantity { get; } = quantity;
    
}