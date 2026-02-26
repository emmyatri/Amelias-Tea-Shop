using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated ApplePay payment.
/// </summary>
public sealed class ApplePayStrategy(string phoneNumber) : PaymentStrategyBase
{
    private readonly string _phoneNumber = 
        string.IsNullOrWhiteSpace(phoneNumber) ? throw new ArgumentException("Phone number cannot be empty.", nameof(phoneNumber)) :
        phoneNumber.Length < 4 ? throw new ArgumentException("Phone number must be at least 4 characters.", nameof(phoneNumber)) :
        phoneNumber;

    protected override string GetPaymentDetail()
        => $"ApplePay Phone Number ending in [{_phoneNumber[^4..]}]";
}