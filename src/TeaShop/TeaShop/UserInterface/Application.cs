using TeaShop.Domain.Inventory;
using TeaShop.Domain.InventoryQuery;
using TeaShop.UserInterface.PaymentBuilder;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface;


/// <summary>
///     Top-level orchestrator for the tea shop. Coordinates the
///     search-display-purchase loop without owning any business logic.
/// </summary>
public sealed class Application
{
    private readonly UserPrompt _reader;
    private readonly TextWriter _writer;
    private readonly InventoryRepository _repository;
    private readonly InventoryQueryBuilder _queryBuilder;
    private readonly InventoryQueryOutputWriter _outputWriter;
    private readonly IReadOnlyList<IPaymentBuilder> _paymentMethods;

    public Application(TextReader reader, TextWriter writer)
    {
        ArgumentNullException.ThrowIfNull(reader);
        ArgumentNullException.ThrowIfNull(writer);

        _writer = writer;
        _reader = new UserPrompt(reader, writer);
        _repository = new InventoryRepository();
        _queryBuilder = new InventoryQueryBuilder(_reader, _repository);
        _outputWriter = new InventoryQueryOutputWriter(_writer);
        _paymentMethods = PaymentBuilderListFactory.Create();
    }
    
    public void Run()
    {
        _writer.WriteLine("===WELCOME TO AMELIA'S TEA SHOP===");
        _writer.WriteLine();
        _writer.WriteLine("Complete the prompts to search our selection of fine teas.");
        _writer.WriteLine();

        var searchAgain = true;

        while (searchAgain)
        {
            var query = _queryBuilder.Build();
            var output = InventoryQueryOutput.From(query);
            _outputWriter.Write(output);

            if (output.Items.Count > 0)
                ProcessPurchase(output);

            searchAgain = _reader.ReadChoice("Search for more tea? (Y/N, default Y)", "Y") != "N";
        }
    }
    
    private void ProcessPurchase(InventoryQueryOutput output)
    {
        var itemIndex = _reader.ReadInt(
            $"Purchase an item? Enter item number 1 - {output.Items.Count} or 0 to continue",
            0, 0, output.Items.Count);
        if (itemIndex == 0) return;

        var selectedItem = output.Items[itemIndex - 1];

        if (!selectedItem.IsAvailable)
        {
            _writer.WriteLine();
            _writer.WriteLine($"\"{selectedItem.Name}\" is out of stock and cannot be purchased.");
            return;
        }

        var quantity = _reader.ReadInt(
            $"Quantity for \"{selectedItem.Name}\" (1 - {selectedItem.Quantity}): ",
            0, 1, selectedItem.Quantity);

        ProcessCheckout(selectedItem, quantity);
        _repository.DecreaseQuantity(selectedItem.Id, quantity);
    }
    
    private void ProcessCheckout(QueriedInventoryItem item, int quantity)
    {
        _writer.WriteLine();
        _writer.WriteLine($"*** Total Price: {item.Price * quantity:C} ***");
        _writer.WriteLine("*** Choose a payment method: ");
        for (var i = 0; i < _paymentMethods.Count; i++)
            _writer.WriteLine($"{i + 1}. {_paymentMethods[i].Name}");
        _writer.WriteLine();

        var selection = _reader.ReadInt("Selection: ", 1, 1, _paymentMethods.Count);
        var strategy = _paymentMethods[selection - 1].Build(_reader, _writer);
        strategy.Checkout(item, quantity, _writer);
        _writer.WriteLine($"*** Purchase complete. Your {quantity} packages of {item.Name} is on the way ***");
    }


}

