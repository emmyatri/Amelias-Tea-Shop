using TeaShop.Domain.Payment;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a wallet number and builds a <see cref="CryptoCurrencyStrategy"/>.
/// </summary>
public class CryptoCurrencyPaymentBuilder(UserPrompt reader, TextWriter writer) : IPaymentBuilder
{
    public string Name => "Crypto Currency";
    private readonly UserPrompt _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));


    public IPaymentStrategy Build(PurchaseDetails purchase)
    {
        string walletNumber;
        do
        {
            walletNumber = _reader.ReadString("Enter CryptoCurrency Wallet Number: ");
            if (walletNumber.Length < 6) _writer.Write("Invalid Wallet Number. Please enter a valid Wallet Number: ");
        } while (walletNumber.Length < 6);

        return new CryptoCurrencyStrategy(purchase, walletNumber);
    }
}