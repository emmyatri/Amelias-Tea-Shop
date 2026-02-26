namespace TeaShop.UserInterface.PaymentBuilder;

/// <summary>
///     Creates the list of available payment builders.
///     Add new payment methods here without modifying existing builders (OCP).
/// </summary>
public static class PaymentBuilderListFactory
{
    public static IReadOnlyList<IPaymentBuilder> Create(IUserPrompt reader)
    {
        return new List<IPaymentBuilder>
        {
            new CreditCardPaymentBuilder(reader),
            new ApplePayPaymentBuilder(reader),
            new CryptoCurrencyPaymentBuilder(reader)
        }.AsReadOnly();
    }
}