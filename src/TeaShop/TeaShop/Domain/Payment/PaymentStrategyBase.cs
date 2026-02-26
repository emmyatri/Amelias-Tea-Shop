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
        
        var total = item.PriceFor(quantity);
        return new PaymentResult(total, GetPaymentMethod(), GetMaskedIdentifier());
    }

    protected abstract string GetPaymentMethod();
    
    protected abstract string GetMaskedIdentifier();
}