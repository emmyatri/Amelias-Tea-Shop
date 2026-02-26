using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a phone number and builds an <see cref="ApplePayStrategy"/>.
/// </summary>
public sealed class ApplePayPaymentBuilder : IPaymentBuilder
{
    public string Name => "ApplePay";
    


    public IPaymentStrategy Build(IUserPrompt reader, TextWriter writer)
    {
        string phoneNumber;
        do
        {
            phoneNumber = reader.ReadString("Enter ApplePay Phone Number: ");
            if (phoneNumber.Length < 10) writer.Write("Invalid Phone Number. Please enter a valid Phone Number: ");
        } while (phoneNumber.Length < 10);

        return new ApplePayStrategy(phoneNumber);
    }
}