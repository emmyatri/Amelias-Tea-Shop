namespace TeaShop.Domain.Payment;


/// <summary>
/// Represents the details of a customer's purchase,
/// shared across all payment strategies.
/// </summary>
public class PurchaseDetails(decimal price, string itemName, int quantity)
{
    public decimal Price { get; } = price;
    public string ItemName { get; } = itemName;
    public int Quantity { get; } = quantity;
    
}