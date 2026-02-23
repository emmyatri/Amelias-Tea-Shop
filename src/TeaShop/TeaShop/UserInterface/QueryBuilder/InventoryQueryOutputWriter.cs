using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;

public class InventoryQueryOutputWriter(TextWriter writer)
{
    private readonly TextWriter _writer = writer;

    public void Write(InventoryQueryOutput output)

    {
        _writer.WriteLine();
        _writer.WriteLine("Applied Filters and Sorts: ");
        foreach (var filter in output.AppliedFilter)
        {
            _writer.WriteLine($"- {filter}");
        }
        
        _writer.WriteLine();

        if (output.Items.Count == 0)
        {
            _writer.WriteLine("No items matched your query. ");
            return;
        }
        
        _writer.WriteLine($"{output.Items.Count} items matched your query: ");
        for (var i = 0; i < output.Items.Count; i++)
        {
            var item = output.Items[i].Item;
            var quantity = item.Quantity > 0 ? $"Qty: {item.Quantity}" : "(OUT OF STOCK)";
            _writer.WriteLine($"{i + 1}. {item.Name, -50} {item.Price, 8:C} {quantity, -15} {item.StarRating}");
        }
    }
}