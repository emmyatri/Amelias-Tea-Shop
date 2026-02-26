using TeaShop.Domain.Inventory;

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


    protected override IReadOnlyList<InventoryItem> Decorate(IReadOnlyList<InventoryItem> items)
    {
        if (string.IsNullOrWhiteSpace(_searchText)) return items;
        
        return items.Where(item => 
            item.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}