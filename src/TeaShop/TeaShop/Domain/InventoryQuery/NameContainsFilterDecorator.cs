namespace TeaShop.Domain.InventoryQuery;

/// <summary>
///     Filters inventory items whose name contains the search text (case-insensitive).
/// </summary>
public sealed class NameContainsFilterDecorator(IInventoryQuery inner, string searchText) : InventoryQueryDecoratorBase(inner)
{
    private readonly string _searchText = searchText ?? throw new ArgumentNullException(nameof(searchText));
    protected override FilterDescription? AppliedDescription
        => string.IsNullOrWhiteSpace(_searchText) ? null 
            : new("Filter", $"Name contains \"{_searchText}\"");
    
    
    public override IReadOnlyList<QueriedInventoryItem> Execute()
    {

        if (string.IsNullOrWhiteSpace(_searchText))
            return _inner.Execute();
        
        var nameContainsResults = _inner.Execute();
        return nameContainsResults.Where(item =>
            item.Item.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}