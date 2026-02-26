namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated ApplePay payment.
/// </summary>
public sealed class ApplePayStrategy(string phoneNumber) : PaymentStrategyBase
{
    public const int RequiredLength = 10;
    
    private readonly string _phoneNumber = 
        string.IsNullOrWhiteSpace(phoneNumber) ? throw new ArgumentException("Phone number cannot be empty.", nameof(phoneNumber)) :
        phoneNumber.Length != RequiredLength ? throw new ArgumentException("Phone number must be exactly 10 characters.", nameof(phoneNumber)) :
        phoneNumber;

    protected override string GetPaymentDetail()
        => $"ApplePay Phone Number ending in [{_phoneNumber[^4..]}]";
}