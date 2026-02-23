namespace TeaShop.Domain.Payment;


/// <summary>
/// Base class for all payment strategies. Holds shared purchase
/// details and provides a common confirmation message format.
/// </summary>
public abstract class PaymentStrategyBase (PurchaseDetails purchase) : IPaymentStrategy
{

    protected readonly PurchaseDetails _purchase = purchase;
    
    public abstract string Checkout();

    
    /// <summary>
    /// Builds a standardized checkout confirmation message
    /// using the provided payment-specific detail.
    /// </summary>
    protected string FormatConfirmation(string paymentDetail)
    {
        return $"*** Total Price: {_purchase.Price:C} ***\n" +
               $"*** Purchase complete via {paymentDetail}. " +
               $"Your package of {_purchase.Quantity} {_purchase.ItemName} is on the way ***";
    }
    
}