using TeaShop.Domain.Inventory;
using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UnitTest.Domain.InventoryQuery;

public class AllInventoryQueryTests
{

    [Fact]

    public void Execute_ReturnsAllItemsFromRepository()
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        
        var result = query.Execute();
        
        Assert.Equal(repository.Items.Count, result.Count);
    }
    
    [Fact]
    public void AppliedFiltersAndSorts_IsEmpty()
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        
        Assert.Empty(query.AppliedFiltersAndSorts);
    }
    
}