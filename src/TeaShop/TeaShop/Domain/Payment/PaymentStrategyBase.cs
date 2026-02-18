namespace TeaShop.Domain.Payment;

public abstract class PaymentStrategyBase : IPaymentStrategy
{
    protected readonly decimal _price;

    protected PaymentStrategyBase(decimal price)
    {
        _price = price;
    }

    public abstract void Checkout();
}