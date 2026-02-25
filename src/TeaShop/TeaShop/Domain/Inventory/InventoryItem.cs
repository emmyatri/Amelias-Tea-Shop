namespace TeaShop.Domain.Inventory;

/// <summary>
///     Represents a single tea product in the inventory.
/// </summary>
public sealed class InventoryItem(Guid id, string name, decimal price, int quantity, StarRating starRating)
{
    public Guid Id { get; } = id;

    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));

    public decimal Price { get; } = price;

    public int Quantity { get; private set; } = quantity;

    public StarRating StarRating { get; } = starRating ?? throw new ArgumentNullException(nameof(starRating));


    /// <summary>
    ///     Reduces available stock after a purchase.
    /// </summary>
    /// <param name="amount">The number of units purchased.</param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the amount exceeds available stock.
    /// </exception>
    public void ReduceQuantity(int amount)
    {
        if (amount > Quantity)
            throw new InvalidOperationException("Not enough in stock.");

        Quantity -= amount;
    }
}