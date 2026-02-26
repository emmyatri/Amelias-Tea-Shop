using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.Payment;

/// <summary>
///     Base class for all payment strategies. Computes the total,
///     delegates to subclasses for payment-specific detail, and
///     returns a structured result.
/// </summary>
public abstract class PaymentStrategyBase : IPaymentStrategy
{

    public PaymentResult Checkout(InventoryItem item, int quantity)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");
        
        var total = item.Price * quantity;
        var detail = GetPaymentDetail();
        return new PaymentResult(total, detail);
    }

    protected abstract string GetPaymentDetail();
}