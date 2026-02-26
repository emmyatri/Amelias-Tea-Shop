namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated CryptoCurrency payment
/// </summary>
public sealed class CryptoCurrencyStrategy(string walletNumber) : PaymentStrategyBase
{
    private readonly string _walletNumber = 
        string.IsNullOrWhiteSpace(walletNumber) ? throw new ArgumentException("Wallet number cannot be empty.", nameof(walletNumber)) :
        walletNumber.Length < 6 ? throw new ArgumentException("Wallet number must be at least 6 characters.", nameof(walletNumber)) :
        walletNumber;

    protected override string GetPaymentDetail()
        => $"CryptoCurrency wallet ending in [{_walletNumber[^6..]}]";
}