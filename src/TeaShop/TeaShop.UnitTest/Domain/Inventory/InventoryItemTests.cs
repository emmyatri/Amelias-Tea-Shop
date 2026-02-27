using TeaShop.Domain.Inventory;

namespace TeaShop.UnitTest.Domain.Inventory;

public class InventoryItemTests
{
    
    [Theory]
    [InlineData(10, true)]
    [InlineData(1, true)]
    [InlineData(0, false)]

    public void IsAvailable_ReturnsExpected(int quantity, bool expected)
    {
        var item = new InventoryItem(Guid.NewGuid(), "Tea", 10m, quantity, new StarRating(3));
        Assert.Equal(expected, item.IsAvailable);
    }
    

    [Fact]

    public void PriceFor_ValidQuantity_ComputeTotal()
    {
        var item = new InventoryItem(Guid.NewGuid(), "Tea", 15m, 10, new StarRating(3));
        Assert.Equal(30m, item.PriceFor(2));
    }
    

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]

    public void PriceFor_NonPositiveQuantity_Throws(int quantity)
    {
        var item = new InventoryItem(Guid.NewGuid(), "Tea", 10m, 10, new StarRating(3));
        Assert.Throws<ArgumentOutOfRangeException>(() => item.PriceFor(quantity));
    }
    

}