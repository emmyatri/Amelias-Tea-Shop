using TeaShop.Domain.Payment;


namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a wallet number and builds a <see cref="CryptoCurrencyStrategy"/>.
/// </summary>
public sealed class CryptoCurrencyPaymentBuilder : IPaymentBuilder
{
    public string Name => "Crypto Currency";


    public IPaymentStrategy Build(IUserPrompt reader, TextWriter writer)
    {
        string walletNumber;
        do
        {
            walletNumber = reader.ReadString("Enter CryptoCurrency Wallet Number: ");
            if (walletNumber.Length < 6) writer.Write("Invalid Wallet Number. Please enter a valid Wallet Number: ");
        } while (walletNumber.Length < 6);

        return new CryptoCurrencyStrategy(walletNumber);
    }
}