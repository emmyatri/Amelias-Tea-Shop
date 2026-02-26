namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated Credit Card payment
/// </summary>
public sealed class CreditCardStrategy(string creditCardNumber) : PaymentStrategyBase
{
    public const int MinLength = 16;
    
    private readonly string _creditCardNumber = 
        string.IsNullOrWhiteSpace(creditCardNumber) ? throw new ArgumentException("Card number cannot be empty.", nameof(creditCardNumber)) :
        creditCardNumber.Length < MinLength ? throw new ArgumentException("Card number must be at least 16 characters.", nameof(creditCardNumber)) :
        creditCardNumber;

    protected override string GetPaymentMethod() => "Credit Card";
    protected override string GetMaskedIdentifier() => _creditCardNumber[^4..];
}