using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a phone number and builds an <see cref="ApplePayStrategy"/>.
/// </summary>
public sealed class ApplePayPaymentBuilder(IUserPrompt reader) : IPaymentBuilder
{
    
    private readonly IUserPrompt _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public string Name => "ApplePay";
    

    public IPaymentStrategy Build()
    {
        string phoneNumber;
        do
        {
            phoneNumber = _reader.ReadString("Enter ApplePay Phone Number: ");
            if (phoneNumber.Length != ApplePayStrategy.RequiredLength) _reader.ShowError("Invalid Phone Number. Please enter a valid Phone Number: ");
        } while (phoneNumber.Length !=  ApplePayStrategy.RequiredLength);

        return new ApplePayStrategy(phoneNumber);
    }
}