using TeaShop.Domain.Inventory;
using TeaShop.Domain.InventoryQuery;
using TeaShop.Domain.InventoryQuery.Filters;
using TeaShop.Domain.InventoryQuery.Sorts;

namespace TeaShop.UnitTest.Domain.InventoryQuery;

public class QueryTests
{
    
    //AllInventoryQuery
    
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
    
    //AvailabilityDecoratorFilter
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AvailabilityFilter_FiltersByAvailability(bool isAvailable)
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        var filter = new AvailabilityFilterDecorator(query, isAvailable);

        var result = filter.Execute();

        Assert.All(result, item => Assert.Equal(isAvailable, item.IsAvailable));
    }
    
    //NameContains
    
    [Fact]
    public void NameContainsFilter_MatchingText_ReturnsFilteredItems()
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        var filter = new NameContainsFilterDecorator(query, "Duck");

        var result = filter.Execute();

        Assert.Single(result);
        Assert.Contains("Duck", result[0].Name);
    }

    [Fact]
    public void NameContainsFilter_NoMatch_ReturnsEmpty()
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        var filter = new NameContainsFilterDecorator(query, "elderberries");

        Assert.Empty(filter.Execute());
    }
    
    //PriceRange
    
    [Fact]
    public void PriceRangeFilter_FiltersWithinBounds()
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        var filter = new PriceRangeFilterDecorator(query, 10.00m, 20.00m);

        var result = filter.Execute();

        Assert.All(result, item =>
        {
            Assert.True(item.Price >= 10.00m);
            Assert.True(item.Price <= 20.00m);
        });
    }
    
    //StarRange
    
    [Fact]
    public void StarRatingRangeFilter_FiltersWithinBounds()
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        var filter = new StarRatingRangeFilterDecorator(
            query, new StarRating(3), new StarRating(5));

        var result = filter.Execute();

        Assert.All(result, item =>
        {
            Assert.True(item.StarRating.StarValue >= 3);
            Assert.True(item.StarRating.StarValue <= 5);
        });
    }
    
    //SortByPrice
    
    [Fact]
    public void SortByPrice_Descending_SortsHighToLow()
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        var sort = new SortByPriceDecorator(query, SortDirection.Descending);

        var result = sort.Execute();

        for (var i = 1; i < result.Count; i++)
            Assert.True(result[i].Price <= result[i - 1].Price);
    }
    
    //SortByStarRating
    
    [Fact]
    public void SortByStarRating_Descending_SortsHighToLow()
    {
        var repository = new InventoryRepository();
        var query = new AllInventoryQuery(repository);
        var sort = new SortByStarRatingDecorator(query, SortDirection.Descending);

        var result = sort.Execute();

        for (var i = 1; i < result.Count; i++)
            Assert.True(result[i].StarRating.CompareTo(result[i - 1].StarRating) <= 0);
    }
    
    //CombinedQueries
    
    [Fact]
    public void CombinedQueries_FiltersAndSortsTogether()
    {
        var repository = new InventoryRepository();
        IInventoryQuery query = new AllInventoryQuery(repository);
        query = new AvailabilityFilterDecorator(query, true);
        query = new PriceRangeFilterDecorator(query, 10.00m, 25.00m);
        query = new SortByPriceDecorator(query, SortDirection.Ascending);

        var result = query.Execute();

        Assert.All(result, item =>
        {
            Assert.True(item.IsAvailable);
            Assert.InRange(item.Price, 10.00m, 25.00m);
        });

        for (var i = 1; i < result.Count; i++)
            Assert.True(result[i].Price >= result[i - 1].Price);

        Assert.Equal(3, query.AppliedFiltersAndSorts.Count);
    }
    
    
}