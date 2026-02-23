using TeaShop.Domain.Inventory;
using TeaShop.Domain.Payment;
using TeaShop.UserInterface.QueryBuilder;
using TeaShop.UserInterface.PaymentBuilder;

namespace TeaShop.UserInterface;

public class Application(TextReader reader, TextWriter writer, InventoryRepository repository)
{

    private readonly TextReader _reader = reader;
    private readonly TextWriter _writer = writer;
    private readonly InventoryRepository _repository = repository;

    public void Run()

    {
        _writer.WriteLine("===WELCOME TO AMELIA'S TEA SHOP===");
        _writer.WriteLine();
        _writer.WriteLine("Complete the prompts to search our selection of fine teas.");
        _writer.WriteLine();

        var searchAgain = true;
        while (searchAgain)
        {
            var output = new InventoryQueryBuilder(_reader, _writer, _repository).Build();
            
            var outputWriter = new InventoryQueryOutputWriter(_writer);
            outputWriter.Write(output);

            if (output.Items.Count > 0)
                HandlePurchase(output);
            
            _writer.WriteLine();
            _writer.Write("Search for more tea? (Y/N, default Y): ");
            var again = _reader.ReadLine().Trim().ToUpper() ?? "";
            searchAgain = again != "N";

        }
    }

    private void HandlePurchase(InventoryQueryOutput output)
    {
        
        _writer.WriteLine();
        _writer.Write($"Purchase an item? Enter item number 1-{output.Items.Count} or 0 to continue (default): ");
        var itemInput = _reader.ReadLine()?.Trim() ?? "";

        if (itemInput == "" || itemInput == "0")
            return;

        var itemIndex = int.Parse(itemInput) - 1;
        var selectedItem = output.Items[itemIndex].Item;

        int quantity;

        do
        {
            _writer.Write($"Quantity for \"{selectedItem.Name}\" (1-{selectedItem.Quantity}): ");
            var quantityInput = _reader.ReadLine()?.Trim() ?? "";
            quantity = int.Parse(quantityInput);
            if (quantity < 1 || quantity > selectedItem.Quantity)
                _writer.Write("Invalid. Quantity must be between 1 and " + selectedItem.Quantity);
        } while (quantity < 1 || quantity > selectedItem.Quantity);
        
        var totalPrice = selectedItem.Price * quantity;
        _writer.WriteLine($"*** Total price: {totalPrice:C} ***");

        var factory = new PaymentBuilderListFactory();
        var builders = factory.Create(_reader, _writer);
        
        _writer.WriteLine("*** Choose a payment method: ");
        for (var i = 0; i < builders.Count; i++)
        {
            _writer.WriteLine($"{i + 1}. {builders[i].Name}");
        }

        _writer.Write("Selection: ");
        var paymentInput = _reader.ReadLine()?.Trim() ?? "";
        var paymentIndex = int.Parse(paymentInput) - 1;
        
        var purchase = new PurchaseDetails(totalPrice, selectedItem.Name, quantity);
        var strategy = builders[paymentIndex].Build(purchase);
        _writer.WriteLine(strategy.Checkout());
        
        _repository.UpdateQuantity(selectedItem.Id, quantity);


    }
}
