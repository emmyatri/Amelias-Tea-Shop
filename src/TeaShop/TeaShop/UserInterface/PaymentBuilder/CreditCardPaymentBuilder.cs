using TeaShop.Domain.Payment;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface.PaymentBuilder;

public class CreditCardPaymentBuilder(QueryInputReader reader, TextWriter writer) : IPaymentBuilder
{
    public string Name => "Credit Card";
    private readonly QueryInputReader _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));
    
    public IPaymentStrategy Build(PurchaseDetails purchase)
    {
        string cardNumber;
        do
        {
            cardNumber = _reader.ReadString("Enter Credit Card Number: ");
            if (cardNumber.Length < 16)
            {
                _writer.Write("Invalid Card Number. Please enter a valid Card Number: ");
            }
        } while (cardNumber.Length < 16);
        
        return new CreditCardStrategy(purchase, cardNumber);
    }
}