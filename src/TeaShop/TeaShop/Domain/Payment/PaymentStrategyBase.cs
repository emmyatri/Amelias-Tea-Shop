namespace TeaShop.Domain.Payment;

public abstract class PaymentStrategyBase (decimal price) : IPaymentStrategy
{
    protected readonly decimal _price = price;

    public abstract void Checkout();
    
}