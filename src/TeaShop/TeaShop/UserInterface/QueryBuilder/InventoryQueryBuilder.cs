using TeaShop.Domain.Inventory;
using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;

public class InventoryQueryBuilder(QueryInputReader reader, InventoryRepository repository)
{
    private readonly QueryInputReader _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly InventoryRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public InventoryQueryOutput Build()
    {
        var context = new QueryContext(new AllInventoryQuery(_repository), new List<string>());
        
        context = ApplyNameFilter(context);
        context = ApplyAvailabilityFilter(context);
        context = ApplyPriceRangeFilter(context);
        context = ApplyStarRatingFilter(context);
        context = ApplyPriceSort(context);
        context = ApplyStarRatingSort(context);

        var results = context.Query.Execute();
        return new InventoryQueryOutput(results.ToList(), context.AppliedFilters);

    }

    private QueryContext ApplyNameFilter(QueryContext ctx)
    {
        var name = _reader.ReadString("*Tea name contains (leave blank for all names): ");
        if (name == "") return ctx;
        
        ctx.AppliedFilters.Add($"Filter: Name contains \"{name}\"");
        return new QueryContext(new NameContainsFilterDecorator(ctx.Query, name), ctx.AppliedFilters);
    }

    private QueryContext ApplyAvailabilityFilter(QueryContext ctx)
    {
        var choice = _reader.ReadChoice("*Is available? (Y/N, default Y): ", "Y");
        var isAvailable = choice != "N";
        
        ctx.AppliedFilters.Add(isAvailable
        ? "Filter: Availability = In Stock (Quantity > 0)"
        : "Filter: Availability = Out of Stock (Quantity = 0)");
        
        return new QueryContext(new AvailabilityFilterDecorator(ctx.Query, isAvailable), ctx.AppliedFilters);
    }

    private QueryContext ApplyPriceRangeFilter(QueryContext ctx)
    {
        var min = _reader.ReadDecimal("* Price minimum (default $0): ", 0m);
        var max = _reader.ReadDecimal("* Price maximum (default $1000): ", 1000m);
        
        ctx.AppliedFilters.Add($"Filter price range between {min} and {max}");
        
        return new QueryContext(new PriceRangeFilterDecorator(ctx.Query, min, max), ctx.AppliedFilters);
    }

    private QueryContext ApplyStarRatingFilter(QueryContext ctx)
    {
        var min = _reader.ReadInt("* Star rating minimum (1-5, default 3): ", 3);
        var max = _reader.ReadInt("* Star rating maximum (1-5, default 5): ", 5);
        
        ctx.AppliedFilters.Add($"Filter star rating between {min} and {max}");
        
        return new QueryContext(
            new StarRatingRangeFilterDecorator(ctx.Query, new StarRating(min),new StarRating(max)), 
            ctx.AppliedFilters);
    }

    private QueryContext ApplyPriceSort(QueryContext ctx)
    {
        var choice = _reader.ReadChoice("* Sort by Price(A/D, default A): ", "A");
        var direction = choice == "D" ? SortDirection.Descending : SortDirection.Ascending;
        
        ctx.AppliedFilters.Add($"Sort: Price ({direction.ToString().ToLower()})");
        return new QueryContext(new SortByPriceDecorator(ctx.Query, direction), ctx.AppliedFilters);
    }
    
    private QueryContext ApplyStarRatingSort(QueryContext ctx)
    {
        var choice = _reader.ReadChoice("* Sort by Star Rating (A/D, default D): ", "D");
        var direction = choice == "A" ? SortDirection.Ascending : SortDirection.Descending;
        
        ctx.AppliedFilters.Add($"Sort: Star Rating ({direction.ToString().ToLower()})");
        return new QueryContext(new SortByStarRatingDecorator(ctx.Query, direction), ctx.AppliedFilters);
    }
}