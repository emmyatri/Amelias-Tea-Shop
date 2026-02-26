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
        var detail = GetPaymentDetail();
        return new PaymentResult(total, detail);
    }

    protected abstract string GetPaymentDetail();
}