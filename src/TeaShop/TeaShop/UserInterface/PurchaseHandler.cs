using TeaShop.Domain.Inventory;
using TeaShop.Domain.InventoryQuery;
using TeaShop.UserInterface.PaymentBuilder;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface;

public sealed class PurchaseHandler(
    IUserPrompt reader,
    TextWriter writer,
    InventoryRepository repository,
    IReadOnlyList<IPaymentBuilder> paymentMethods)
{

    private readonly IUserPrompt _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly TextWriter _writer =
        writer ?? throw new ArgumentNullException(nameof(writer));
    private readonly InventoryRepository _repository = 
        repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IReadOnlyList<IPaymentBuilder> _paymentMethods = 
        paymentMethods ?? throw new ArgumentNullException(nameof(paymentMethods));

    public void Handle(InventoryQueryOutput output)
    {
        var itemIndex = _reader.ReadInt(
            $"\nPurchase an item? Enter item number 1 - {output.Items.Count} or 0 to continue: ", 0, 0, output.Items.Count);
        if (itemIndex == 0) return;
        
        var selectedItem = output.Items[itemIndex-1];

        if (!selectedItem.IsAvailable)
        {
            _writer.WriteLine();
            _writer.WriteLine($"\"{selectedItem.Name}\" is out of stock and cannot be purchased.");
            return;
        }
        
        var quantity = _reader.ReadInt( 
            $"Quantity for \"{selectedItem.Name}\" (1 - {selectedItem.Quantity}): ",
            null, 1, selectedItem.Quantity);
        
        ProcessCheckout(selectedItem, quantity);
        _repository.DecreaseQuantity(selectedItem.Id, quantity);
        
        
    }
    
    private void ProcessCheckout(QueriedInventoryItem item, int quantity)
    {
        _writer.WriteLine();
        _writer.WriteLine($"*** Total Price: {item.PriceFor(quantity):C} ***");
        _writer.WriteLine("*** Choose a payment method: ");
        for (var i = 0; i < _paymentMethods.Count; i++)
            _writer.WriteLine($"{i + 1}. {_paymentMethods[i].Name}");
        _writer.WriteLine();

        while (true)
        {
            var input = _reader.ReadChoice(
                $"Selection (1 - {_paymentMethods.Count}), or \"cancel\" to cancel): ","");

            if (input.Equals("CANCEL", StringComparison.OrdinalIgnoreCase))
            {
                _writer.WriteLine("*** Payment cancelled ***");
                return;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                _reader.ShowError("No payment method selected. Please enter a valid payment method or enter \"cancel\" to cancel.");
                continue;
            }
            
            if (int.TryParse(input, out var selection) && selection >= 1 && selection <= _paymentMethods.Count)
            {
                var strategy = _paymentMethods[selection - 1].Build();
                var result = strategy.Checkout(item, quantity);
                _writer.WriteLine($"*** Paid {result.Total:C} via {result.PaymentMethod} ending in [{result.MaskedIdentifier}] ***");
                _writer.WriteLine($"*** Purchase complete. Your {quantity} packages of {item.Name} is on the way ***");
                return;
            }
            
            _reader.ShowError($"Invalid selection. Enter 1-{_paymentMethods.Count} or \"cancel\" to cancel.");
        }
    }
    
}