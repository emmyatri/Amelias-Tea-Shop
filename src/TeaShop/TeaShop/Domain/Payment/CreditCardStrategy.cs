namespace TeaShop.Domain.Payment;

public class CreditCardStrategy(PurchaseDetails purchase, string creditCardNumber) : PaymentStrategyBase (purchase)
{
    private readonly string _creditCardNumber = creditCardNumber;

    public override string Checkout()
    {
        return FormatConfirmation($"Credit Card ending in [{_creditCardNumber[^4..]}]");
    }
    
}