namespace TeaShop.Domain.InventoryQuery;

public class NameContainsFilterDecorator(IInventoryQuery inner, string searchText): InventoryQueryDecoratorBase (inner)
{
    private readonly string _searchText = searchText;

    public override List<QueriedInventoryItem> Execute()
    {
        var nameContainsResults = _inner.Execute();

        return nameContainsResults.Where(item =>
            item.Item.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}