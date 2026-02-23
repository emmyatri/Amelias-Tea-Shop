namespace TeaShop.Domain.Payment;

public class CryptoCurrencyStrategy(PurchaseDetails purchase, string walletNumber) : PaymentStrategyBase (purchase)
{
    private readonly string _walletNumber = walletNumber;

    public override string Checkout()
    {
        return FormatConfirmation($"CryptoCurrency wallet ending in [{_walletNumber[^6..]}]");
    }
    
}