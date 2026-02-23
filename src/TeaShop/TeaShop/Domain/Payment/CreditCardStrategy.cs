namespace TeaShop.Domain.Payment;


/// <summary>
/// Processes a simulated Credit Card payment
/// </summary>
public class CreditCardStrategy(PurchaseDetails purchase, string creditCardNumber) : PaymentStrategyBase (purchase)
{
    private readonly string _creditCardNumber = creditCardNumber;

    public override string Checkout()
    {
        return FormatConfirmation($"Credit Card ending in [{_creditCardNumber[^4..]}]");
    }
    
}