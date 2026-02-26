using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a card number and builds a <see cref="CreditCardStrategy"/>.
/// </summary>
public sealed class CreditCardPaymentBuilder(IUserPrompt reader) : IPaymentBuilder
{
    private readonly IUserPrompt _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public string Name => "Credit Card";

    public IPaymentStrategy Build()
    {
        string cardNumber;
        do
        {
            cardNumber = _reader.ReadString("Enter Credit Card Number: ");
            if (cardNumber.Length < CreditCardStrategy.MinLength) _reader.ShowError("Invalid Card Number. Please enter a valid Card Number: ");
        } while (cardNumber.Length < CreditCardStrategy.MinLength);

        return new CreditCardStrategy(cardNumber);
    }
}