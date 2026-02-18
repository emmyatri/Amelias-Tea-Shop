using TeaShop.Domain.Inventory;
using System.Linq;

namespace TeaShop.Domain.InventoryQuery;

public class AllInventoryQuery(InventoryRepository stock) : IInventoryQuery
{
    private InventoryRepository _stock = stock;

    public List<QueriedInventoryItem> Execute()
    {
        return
        _stock.Items.Select((item, index) => 
            new QueriedInventoryItem(item, index + 1)).ToList();
    }
}