using TeaShop.Domain.Inventory;
using TeaShop.Domain.Payment;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface.PurchaseBuilder;

public class PurchaseHandler(QueryInputReader reader, PaymentHandler paymentHandler, 
    InventoryRepository repository, TextWriter writer)
{
    
    private readonly QueryInputReader _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly PaymentHandler _paymentHandler = paymentHandler ?? throw new ArgumentNullException(nameof(paymentHandler));
    private readonly InventoryRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));

    public void TryPurchase(InventoryQueryOutput output)
    {
        var itemIndex = _reader.ReadInt(
            $"Purchase an item? Enter item number 1 - {output.Items.Count} or 0 to continue", 0);
        if (itemIndex == 0) return;
        
        var selectedItem =  output.Items[itemIndex-1].Item;

        if (selectedItem.Quantity == 0)
        {
            _writer.WriteLine();
            _writer.WriteLine($"\"{selectedItem.Name}\" is out of stock and cannot be purchased.");
            return;
        }
        
        var quantity = ReadValidQuantity(selectedItem);
        var totalPrice = selectedItem.Price * quantity;
        
        _writer.WriteLine();
        _writer.WriteLine($"*** Total price: {totalPrice:C} ***");
        
        paymentHandler.ProcessPayment(totalPrice, selectedItem.Name, quantity);
        _repository.UpdateQuantity(selectedItem.Id, quantity);
    }

    private int ReadValidQuantity(InventoryItem item)
    {
        int quantity;

        do
        {
            quantity = _reader.ReadInt($"Quantity for \"{item.Name}\" (1 - {item.Quantity}): ", 0);
            if (quantity < 1 || quantity > item.Quantity)
            {
                _writer.WriteLine($"Invalid selection. Quantity must be between 1 and {item.Quantity}. Re-enter quantity \n");
            }
        } while (quantity < 1 || quantity > item.Quantity);
        
        return quantity;
    }
}