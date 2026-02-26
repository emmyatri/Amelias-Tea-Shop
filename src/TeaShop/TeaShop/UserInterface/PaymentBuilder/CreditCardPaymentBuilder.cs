using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a card number and builds a <see cref="CreditCardStrategy"/>.
/// </summary>
public sealed class CreditCardPaymentBuilder : IPaymentBuilder
{
    public string Name => "Credit Card";

    public IPaymentStrategy Build(IUserPrompt reader, TextWriter writer)
    {
        string cardNumber;
        do
        {
            cardNumber = reader.ReadString("Enter Credit Card Number: ");
            if (cardNumber.Length < 16) writer.Write("Invalid Card Number. Please enter a valid Card Number: ");
        } while (cardNumber.Length < 16);

        return new CreditCardStrategy(cardNumber);
    }
}