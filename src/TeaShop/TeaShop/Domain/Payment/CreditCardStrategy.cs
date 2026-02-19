namespace TeaShop.Domain.Payment;

public class CreditCardStrategy(decimal price, string creditCardNumber) : PaymentStrategyBase (price)
{
    private readonly string _creditCardNumber =  creditCardNumber;
    
    public override void Checkout() { }
    
}