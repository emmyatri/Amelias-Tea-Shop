using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.Payment;

/// <summary>
///     Base class for all payment strategies. Holds shared purchase
///     details and provides a common confirmation message format.
/// </summary>
public abstract class PaymentStrategyBase : IPaymentStrategy
{

    public void Checkout(InventoryItem item, int quantity, TextWriter writer)
    {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentNullException.ThrowIfNull(writer);
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");
        
        var total = ComputeTotal(item,  quantity);
        var detail = GetPaymentDetail();
        writer.WriteLine($"*** Paid {total:C} via {detail} ***");
    }

    protected decimal ComputeTotal(InventoryItem item, int quantity)
    {
        return item.Price * quantity;
    }

    protected abstract string GetPaymentDetail();
}