namespace TeaShop.Domain.InventoryQuery;

public class NameContainsFilterDecorator : InventoryQueryDecoratorBase
{
    private readonly string _searchText;
    
    public NameContainsFilterDecorator(IInventoryQuery inner, string searchText) : base(inner)
        {
        _searchText = searchText;
        }

    public override List<QueriedInventoryItem> Execute()
    {

        var nameContainsResults = _inner.Execute();

        return nameContainsResults.Where(item =>
            item.Item.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();

    }
}