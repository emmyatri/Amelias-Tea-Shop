namespace TeaShop.Domain.Payment;

public class CreditCardStrategy : PaymentStrategyBase
{
    private readonly string _creditCardNumber;

    public CreditCardStrategy(decimal price, string creditCardNumber) : base(price)
    {
        _creditCardNumber = creditCardNumber;
    }

    public override void Checkout()
    { }
    
}