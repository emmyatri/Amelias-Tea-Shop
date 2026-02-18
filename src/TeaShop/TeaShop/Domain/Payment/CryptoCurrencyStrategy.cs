namespace TeaShop.Domain.Payment;

public class CryptoCurrencyStrategy : PaymentStrategyBase
{
    private readonly string _walletNumber;

    public CryptoCurrencyStrategy(decimal price, string walletNumber) : base(price)
    {
        _walletNumber = walletNumber;
    }

    public override void Checkout()
    {
    }
}