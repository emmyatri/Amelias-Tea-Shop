namespace TeaShop.Domain.Payment;

public class ApplePayStrategy(decimal price, string phoneNumber) : PaymentStrategyBase (price)
{
    private readonly string _phoneNumber = phoneNumber;

    public override void Checkout() { }
    
}