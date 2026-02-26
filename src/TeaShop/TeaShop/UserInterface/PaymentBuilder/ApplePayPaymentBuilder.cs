using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a phone number and builds an <see cref="ApplePayStrategy"/>.
/// </summary>
public sealed class ApplePayPaymentBuilder : IPaymentBuilder
{
    public string Name => "ApplePay";
    


    public IPaymentStrategy Build(IUserPrompt reader)
    {
        string phoneNumber;
        do
        {
            phoneNumber = reader.ReadString("Enter ApplePay Phone Number: ");
            if (phoneNumber.Length != ApplePayStrategy.RequiredLength) reader.ShowError("Invalid Phone Number. Please enter a valid Phone Number: ");
        } while (phoneNumber.Length !=  ApplePayStrategy.RequiredLength);

        return new ApplePayStrategy(phoneNumber);
    }
}