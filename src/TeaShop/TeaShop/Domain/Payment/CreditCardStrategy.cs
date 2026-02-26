using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated Credit Card payment
/// </summary>
public sealed class CreditCardStrategy(string creditCardNumber) : PaymentStrategyBase
{
    private readonly string _creditCardNumber =
        creditCardNumber ?? throw new ArgumentNullException(nameof(creditCardNumber));

    public override void Checkout(InventoryItem item, int quantity, TextWriter output)
    {
        WritePaymentDetail(output, $"Credit Card ending in [{_creditCardNumber[^4..]}]");
    }
}