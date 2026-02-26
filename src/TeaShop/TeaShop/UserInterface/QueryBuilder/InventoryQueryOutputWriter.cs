namespace TeaShop.UserInterface.QueryBuilder;

/// <summary>
///     Formats and writes query results to the output stream,
///     including applied filters and a numbered item table.
/// </summary>
public sealed class InventoryQueryOutputWriter(TextWriter writer)
{
    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));

    public void Write(InventoryQueryOutput output)

    {
        _writer.WriteLine();
        _writer.WriteLine("Applied Filters and Sorts: ");
        
        foreach (var filter in output.AppliedFilters)
            _writer.WriteLine($"- {filter} ");

        _writer.WriteLine();

        if (output.Items.Count == 0)
        {
            _writer.WriteLine("No items matched your query. ");
            return;
        }

        _writer.WriteLine($"{output.Items.Count} items matched your query: ");

        var maxNameLength = output.Items.Max(i => i.Name.Length) + 2;

        foreach (var item in output.Items)
        {
            var quantity = item.Quantity > 0 ? $"Qty: {item.Quantity,-4}" : "(OUT OF STOCK)";
            _writer.WriteLine(
                $"{item.Index,2}. {item.Name.PadRight(maxNameLength)} {item.Price,15:C} {quantity,-15} {item.StarRating}");
        }
    }
}