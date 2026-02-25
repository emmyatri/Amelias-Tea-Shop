namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Filters inventory items whose name contains the search text (case-insensitive).
/// </summary>
public sealed class NameContainsFilterDecorator(IInventoryQuery inner, string searchText) : InventoryQueryDecoratorBase(inner)
{
    private readonly string _searchText = searchText ?? throw new ArgumentNullException(nameof(searchText));

    public override IReadOnlyList<QueriedInventoryItem> Execute()
    {
        var nameContainsResults = _inner.Execute();

        return nameContainsResults.Where(item =>
            item.Item.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}