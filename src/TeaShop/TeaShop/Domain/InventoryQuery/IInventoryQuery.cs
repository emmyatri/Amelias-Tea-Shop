namespace TeaShop.Domain.InventoryQuery;

public interface IInventoryQuery
{
    IReadOnlyList<QueriedInventoryItem> Execute();
}