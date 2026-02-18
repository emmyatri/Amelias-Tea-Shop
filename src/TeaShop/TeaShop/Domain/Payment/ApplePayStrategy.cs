namespace TeaShop.Domain.Payment;

public class ApplePayStrategy : PaymentStrategyBase
{
    private readonly string _phoneNumber;

    public ApplePayStrategy(decimal price, string phoneNumber) : base(price)
    {
        _phoneNumber = phoneNumber;
    }

    public override void Checkout()
    {
    }
}