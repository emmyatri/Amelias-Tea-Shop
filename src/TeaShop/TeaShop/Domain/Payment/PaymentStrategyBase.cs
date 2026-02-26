using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.Payment;

/// <summary>
///     Base class for all payment strategies. Holds shared purchase
///     details and provides a common confirmation message format.
/// </summary>
public abstract class PaymentStrategyBase : IPaymentStrategy
{

    public abstract void Checkout(InventoryItem item, int quantity, TextWriter writer);


    /// <summary>
    ///     Builds a standardized checkout confirmation message
    ///     using the provided payment-specific detail.
    /// </summary>
    protected void WritePaymentDetail(TextWriter writer, string paymentDetail)
    {
        writer.WriteLine($"*** Paid via {paymentDetail} ***");
    }
}