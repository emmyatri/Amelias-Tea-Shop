namespace TeaShop.Domain.Payment;

/// <summary>
///     Processes a simulated Credit Card payment
/// </summary>
public sealed class CreditCardStrategy(string creditCardNumber) : PaymentStrategyBase
{
    private readonly string _creditCardNumber = 
        string.IsNullOrWhiteSpace(creditCardNumber) ? throw new ArgumentException("Card number cannot be empty.", nameof(creditCardNumber)) :
        creditCardNumber.Length < 4 ? throw new ArgumentException("Card number must be at least 4 characters.", nameof(creditCardNumber)) :
        creditCardNumber;

    protected override string GetPaymentDetail()
        => $"Credit Card ending in [{_creditCardNumber[^4..]}]";
}