namespace TeaShop.Domain.Payment;

public abstract class PaymentStrategyBase (PurchaseDetails purchase) : IPaymentStrategy
{

    protected readonly PurchaseDetails _purchase = purchase;
    
    public abstract string Checkout();

    protected string FormatConfirmation(string paymentDetail)
    {
        return $"*** Total Price: {_purchase.Price:C} ***\n" +
               $"*** Purchase complete via {paymentDetail}. " +
               $"Your package of {_purchase.Quantity} {_purchase.ItemName} is on the way ***";
    }
    
}