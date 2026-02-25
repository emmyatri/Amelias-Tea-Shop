using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface.PaymentBuilder;


/// <summary>
///     Creates the list of available payment builders.
///     Add new payment methods here without modifying existing builders (OCP).
/// </summary>
public class PaymentBuilderListFactory
{
    public IReadOnlyList<IPaymentBuilder> Create(UserPrompt reader, TextWriter writer)
    {
        return new List<IPaymentBuilder>
        {
            new CreditCardPaymentBuilder(reader, writer),
            new ApplePayPaymentBuilder(reader, writer),
            new CryptoCurrencyPaymentBuilder(reader, writer)
        };
    }
}