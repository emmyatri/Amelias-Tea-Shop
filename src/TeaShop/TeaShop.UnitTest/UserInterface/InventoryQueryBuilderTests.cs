using TeaShop.Domain.Inventory;
using TeaShop.UnitTest.UserInterface.Fakes;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UnitTest.UserInterface;

public class InventoryQueryBuilderTests
{
    [Fact]
    public void Build_WithValidInput_ReturnsQueryWithAllDescriptions()
    {
        var prompt = new FakeUserPrompt()
            .WithString("Oolong")
            .WithChoice("Y")
            .WithDecimal(0m)
            .WithDecimal(1000m)
            .WithInt(3)
            .WithInt(5)
            .WithChoice("A")
            .WithChoice("D");

        var repo = new InventoryRepository();
        var builder = new InventoryQueryBuilder(prompt, repo);

        var query = builder.Build();
        var descriptions = query.AppliedFiltersAndSorts;

        Assert.Contains(descriptions, d => d.Contains("Oolong"));
        Assert.Contains(descriptions, d => d.Contains("In Stock"));
        Assert.Contains(descriptions, d => d.Contains("Price range"));
        Assert.Contains(descriptions, d => d.Contains("Star rating between"));
        Assert.Contains(descriptions, d => d.Contains("Sort: Price"));
        Assert.Contains(descriptions, d => d.Contains("Sort: Star Rating"));
    }
}