using TeaShop.Domain.Payment;


namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Collects a wallet number and builds a <see cref="CryptoCurrencyStrategy"/>.
/// </summary>
public sealed class CryptoCurrencyPaymentBuilder(IUserPrompt reader) : IPaymentBuilder
{
    private readonly IUserPrompt _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public string Name => "Crypto Currency";


    public IPaymentStrategy Build()
    {
        string walletNumber;
        do
        {
            walletNumber = _reader.ReadString("Enter CryptoCurrency Wallet Number: ");
            if (walletNumber.Length < CryptoCurrencyStrategy.MinLength) _reader.ShowError("Invalid Wallet Number. Please enter a valid Wallet Number: ");
        } while (walletNumber.Length < CryptoCurrencyStrategy.MinLength);

        return new CryptoCurrencyStrategy(walletNumber);
    }
}