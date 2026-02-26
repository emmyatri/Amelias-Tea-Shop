using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated ApplePay payment.
/// </summary>
public sealed class ApplePayStrategy(string phoneNumber) : PaymentStrategyBase
{
    private readonly string _phoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));

    public override void Checkout(InventoryItem item, int quantity, TextWriter output)
    {
        WritePaymentDetail(output, $"ApplePay Phone Number ending in [{_phoneNumber[^4..]}]");
    }
}