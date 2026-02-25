namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated ApplePay payment.
/// </summary>
public class ApplePayStrategy(PurchaseDetails purchase, string phoneNumber) : PaymentStrategyBase(purchase)
{
    private readonly string _phoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));

    public override string Checkout()
    {
        return FormatConfirmation($"ApplePay associated with phone number ending in [{_phoneNumber[^4..]}]");
    }
}