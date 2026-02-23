using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

public class ApplePayPaymentBuilder(TextReader reader, TextWriter writer) : IPaymentBuilder
{
    public string Name => "ApplePay";
    private readonly TextReader _reader = reader;
    private readonly TextWriter _writer = writer;


    public IPaymentStrategy Build(PurchaseDetails purchase)
    {
        string phoneNumber;
        do
        {
            _writer.Write("Enter Phone Number: ");
            phoneNumber = _reader.ReadLine()?.Trim() ?? "";
            if (phoneNumber.Length < 10)
            {
                _writer.Write("Invalid Phone Number. Please enter a valid Phone Number: ");
            }
        } while (phoneNumber.Length < 10);
        
        return new ApplePayStrategy(purchase, phoneNumber);
    }
}