using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery;

public class AllInventoryQuery(InventoryRepository stock) : IInventoryQuery
{
    private readonly InventoryRepository _stock = stock;

    public List<QueriedInventoryItem> Execute()
    {
        return
            _stock.Items.Select(item =>
                new QueriedInventoryItem(item)).ToList();
    }
}