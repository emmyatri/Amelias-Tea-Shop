using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated CryptoCurrency payment
/// </summary>
public sealed class CryptoCurrencyStrategy(string walletNumber) : PaymentStrategyBase
{
    private readonly string _walletNumber = walletNumber ?? throw new ArgumentNullException(nameof(walletNumber));

    public override void Checkout(InventoryItem item, int quantity, TextWriter output)
    {
        WritePaymentDetail(output, $"CryptoCurrency wallet ending in [{_walletNumber[^6..]}]");
    }
}