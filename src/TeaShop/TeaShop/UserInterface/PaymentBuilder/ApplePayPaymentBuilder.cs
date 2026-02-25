using TeaShop.Domain.Payment;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a phone number and builds an <see cref="ApplePayStrategy"/>.
/// </summary>
public sealed class ApplePayPaymentBuilder(UserPrompt reader, TextWriter writer) : IPaymentBuilder
{
    public string Name => "ApplePay";
    private readonly UserPrompt _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));


    public IPaymentStrategy Build(PurchaseDetails purchase)
    {
        string phoneNumber;
        do
        {
            phoneNumber = _reader.ReadString("Enter ApplePay Phone Number: ");
            if (phoneNumber.Length < 10) _writer.Write("Invalid Phone Number. Please enter a valid Phone Number: ");
        } while (phoneNumber.Length < 10);

        return new ApplePayStrategy(purchase, phoneNumber);
    }
}