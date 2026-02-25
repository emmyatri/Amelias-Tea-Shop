using TeaShop.Domain.Payment;
using TeaShop.UserInterface.PaymentBuilder;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface.PurchaseBuilder;

/// <summary>
///     Displays available payment methods, collects the user's selection,
///     and delegates checkout to the chosen payment builder.
/// </summary>
public sealed class PaymentHandler(UserPrompt reader, TextWriter writer)
{
    private readonly UserPrompt _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));


    public void ProcessPayment(decimal totalPrice, string itemName, int quantity)
    {
        var factory = new PaymentBuilderListFactory();
        var builder = factory.Create(_reader, _writer);

        _writer.WriteLine();
        _writer.WriteLine("*** Choose a payment method: ");
        for (var i = 0; i < builder.Count; i++) _writer.WriteLine($"{i + 1}. {builder[i].Name}");
        _writer.WriteLine();


        var selection = _reader.ReadInt("Selection: ", 1);
        var purchase = new PurchaseDetails(totalPrice, itemName, quantity);
        var strategy = builder[selection - 1].Build(purchase);
        _writer.WriteLine(strategy.Checkout());
    }
}