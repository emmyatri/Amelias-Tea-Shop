using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

public class CryptoCurrencyPaymentBuilder(TextReader reader, TextWriter writer) : IPaymentBuilder
{
    public string Name => "Crypto Currency";
    private readonly TextReader _reader = reader;
    private readonly TextWriter _writer = writer;


    public IPaymentStrategy Build(PurchaseDetails purchase)
    {
        string walletNumber;
        do
        {
            _writer.Write("Enter CryptoCurrency Wallet Number: ");
            walletNumber = _reader.ReadLine()?.Trim() ?? "";
            if (walletNumber.Length < 6)
            {
                _writer.Write("Invalid Wallet Number. Please enter a valid Wallet Number: ");
            }
        } while (walletNumber.Length < 6);
        
        return new CryptoCurrencyStrategy(purchase, walletNumber);
    }
    
}