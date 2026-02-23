using TeaShop.Domain.Inventory;
using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;

public class InventoryQueryBuilder(TextReader reader, TextWriter writer, InventoryRepository repository)
{
    private readonly TextReader _reader = reader;
    private readonly TextWriter _writer = writer;
    private readonly InventoryRepository _repository = repository;

    public InventoryQueryOutput Build()
    {
        
        var appliedFilters = new List<string>();
        
        IInventoryQuery query = new AllInventoryQuery(_repository);
        
        //Name Contains
        _writer.Write("*Tea name contains (leave blank for all names): ");
        var nameInput = _reader.ReadLine()?.Trim() ?? "";
        if (nameInput != "")
        {
            query = new NameContainsFilterDecorator(query, nameInput);
            appliedFilters.Add($"Filter: Name contains \"{nameInput}\"");
        }
        
        
        //Is Available
        _writer.Write("*Is available? (Y/N, default Y): ");
        var isAvailableInput = _reader.ReadLine()?.Trim().ToUpper() ?? "";
        var isAvailable = isAvailableInput != "N";
        
        query = new AvailabilityFilterDecorator(query, isAvailable);
        appliedFilters.Add(isAvailable
            ? $"Filter: Availability = In Stock (Quantity > 0)"
            : "Filter: Availability = Out of Stock (Quantity = 0)");
        
        
        // Price Minimum
        _writer.Write("* Price minimum (default $0): ");
        var priceMinimumInput = _reader.ReadLine()?.Trim() ?? "";
        var priceMin = priceMinimumInput == "" ? 0m : decimal.Parse(priceMinimumInput);
        
        
        // Price Maximum
        _writer.Write("* Price maximum (default $1000): ");
        var priceMaximumInput = _reader.ReadLine()?.Trim() ?? "";
        var priceMax = priceMaximumInput == "" ? 1000m : decimal.Parse(priceMaximumInput);
        
        query = new PriceRangeFilterDecorator(query, priceMin, priceMax);
        appliedFilters.Add($"Filter: Price between {priceMin:C} and {priceMax:C}");
        
        
        // Star Rating Minimum
        _writer.Write("* Star rating minimum (1-5, default 3): ");
        var starRatingMin = _reader.ReadLine()?.Trim() ?? "";
        var starMin = new StarRating(starRatingMin == "" ? 3 : int.Parse(starRatingMin));
        
        // Star Rating Maximum
        _writer.Write("* Star rating maximum (1-5, default 5): ");
        
        var starRatingMax = _reader.ReadLine()?.Trim() ?? "";
        var starMax = new StarRating(starRatingMax == "" ? 5 : int.Parse(starRatingMax));
        
        query = new StarRatingRangeFilterDecorator(query, starMin, starMax);
        appliedFilters.Add($"Filter: Star rating between {starMin.StarValue} and {starMax.StarValue}");
        
        
        //Sort by Price
        _writer.Write("* Sort by Price (A/D, default A): ");
        var sortByPriceInput = _reader.ReadLine()?.Trim().ToUpper() ?? "";
        var priceSortDirection = sortByPriceInput == "D" ? SortDirection.Descending : SortDirection.Ascending;
        
        query = new SortByPriceDecorator(query, priceSortDirection);
        appliedFilters.Add($"Sort: Price ({priceSortDirection.ToString().ToLower()})");
        
        
        //Sort by Star Rating
        _writer.Write("* Sort by Star Rating (A/D, default D): ");
        var sortByStarRatingInput = _reader.ReadLine()?.Trim().ToUpper() ?? "";
        var starSortDirection = sortByStarRatingInput == "A" ? SortDirection.Ascending : SortDirection.Descending;
        
        
        query = new SortByStarRatingDecorator(query, starSortDirection);
        appliedFilters.Add($"Sort: Star Rating ({starSortDirection.ToString().ToLower()})");

        
        //Execute composed query and return results
        var results = query.Execute();
        return new InventoryQueryOutput(results.ToList(), appliedFilters);
    }
    
}