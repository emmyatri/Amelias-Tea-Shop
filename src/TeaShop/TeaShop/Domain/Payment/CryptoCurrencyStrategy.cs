namespace TeaShop.Domain.Payment;

public class CryptoCurrencyStrategy(decimal price, string walletNumber) : PaymentStrategyBase (price)
{
    private readonly string _walletNumber = walletNumber;

    public override void Checkout() { }
    
}