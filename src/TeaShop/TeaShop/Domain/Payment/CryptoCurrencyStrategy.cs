namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated CryptoCurrency payment
/// </summary>
public sealed class CryptoCurrencyStrategy(PurchaseDetails purchase, string walletNumber) : PaymentStrategyBase(purchase)
{
    private readonly string _walletNumber = walletNumber ?? throw new ArgumentNullException(nameof(walletNumber));

    public override string Checkout()
    {
        return FormatConfirmation($"CryptoCurrency wallet ending in [{_walletNumber[^6..]}]");
    }
}