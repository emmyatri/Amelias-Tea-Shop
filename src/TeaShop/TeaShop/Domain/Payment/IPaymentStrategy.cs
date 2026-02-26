using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.Payment;

/// <summary>
///     Defines a payment method using the Strategy pattern.
///     Each implementation handles a specific payment type.
/// </summary>
public interface IPaymentStrategy
{
    /// <summary>
    ///     Executes the simulated checkout and returns a confirmation message.
    /// </summary>
    void Checkout(InventoryItem item, int quantity, TextWriter writer);
}