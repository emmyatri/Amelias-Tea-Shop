using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

public class CreditCardPaymentBuilder(TextReader reader, TextWriter writer) : IPaymentBuilder
{
    public string Name => "Credit Card";
    private readonly TextReader _reader = reader;
    private readonly TextWriter _writer = writer;
    
    public IPaymentStrategy Build(PurchaseDetails purchase)
    {
        string cardNumber;
        do
        {
            _writer.Write("Enter Credit Card Number: ");
            cardNumber = _reader.ReadLine()?.Trim() ?? "";
            if (cardNumber.Length < 16)
            {
                _writer.Write("Invalid Card Number. Please enter a valid Card Number: ");
            }
        } while (cardNumber.Length < 16);
        
        return new CreditCardStrategy(purchase, cardNumber);
    }
}