using TeaShop.Domain.Inventory;

namespace TeaShop.UnitTest.Domain.Inventory;

public class InventoryRepositoryTests
{

    [Fact]
    public void DecreaseQuantity_ValidAmount_UpdatesItem()
    {
        var repository = new InventoryRepository();
        var item = repository.Items.First(i => i.Quantity > 0);
        var originalQuantity = item.Quantity;
        
        repository.DecreaseQuantity(item.Id, 3);
        
        var updated = repository.Items.First(i => i.Id == item.Id);
        
        Assert.Equal(originalQuantity - 3, updated.Quantity);
    }
    
    [Fact]
    public void DecreaseQuantity_ExceedsStock_Throws()
    {
        var repository = new InventoryRepository();
        var item = repository.Items.First(i => i.Quantity > 0);

        Assert.Throws<InvalidOperationException>(
            () => repository.DecreaseQuantity(item.Id, item.Quantity + 1));
    }
    
    [Fact]
    public void DecreaseQuantity_UnknownId_Throws()
    {
        var repository = new InventoryRepository();

        Assert.Throws<ArgumentException>(
            () => repository.DecreaseQuantity(Guid.NewGuid(), 1));
    }
    
}