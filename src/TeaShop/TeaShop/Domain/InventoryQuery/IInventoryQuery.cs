namespace TeaShop.Domain.InventoryQuery;

public interface IInventoryQuery
{
    List<QueriedInventoryItem> Execute();

}