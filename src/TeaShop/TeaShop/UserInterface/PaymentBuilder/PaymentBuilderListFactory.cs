using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

public class PaymentBuilderListFactory
{
    public IReadOnlyList<IPaymentBuilder> Create(TextReader reader, TextWriter writer)
    {
        return new List<IPaymentBuilder>
        {
            new CreditCardPaymentBuilder(reader, writer),
            new ApplePayPaymentBuilder(reader, writer),
            new CryptoCurrencyPaymentBuilder(reader, writer)
        };
    }
}