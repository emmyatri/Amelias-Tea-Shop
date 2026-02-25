using TeaShop.Domain.Inventory;
using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;


/// <summary>
///     Collects user search criteria and constructs a decorated
///     query pipeline of filters and sorts.
/// </summary>
public sealed class InventoryQueryBuilder(UserPrompt reader, InventoryRepository repository)
{
    private readonly UserPrompt _reader = reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly InventoryRepository
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public InventoryQueryOutput Build()
    {
        IInventoryQuery query = new AllInventoryQuery(_repository);

        query = ApplyNameFilter(query);
        query = ApplyAvailabilityFilter(query);
        query = ApplyPriceRangeFilter(query);
        query = ApplyStarRatingFilter(query);
        query = ApplyPriceSort(query);
        query = ApplyStarRatingSort(query);

        var results = query.Execute();

        return new InventoryQueryOutput(results.ToList(), query.AppliedFiltersAndSorts);
    }

    private IInventoryQuery ApplyNameFilter(IInventoryQuery query)
    {
        var name = _reader.ReadString("*Tea name contains (leave blank for all names): ");
        if (string.IsNullOrWhiteSpace(name)) return query;
        return new NameContainsFilterDecorator(query, name);
    }

    private IInventoryQuery ApplyAvailabilityFilter(IInventoryQuery query)
    {
        var choice = _reader.ReadChoice("*Is available? (Y/N, default Y): ", "Y");
        var isAvailable = choice != "N";
        return new AvailabilityFilterDecorator(query, isAvailable);
    }

    private IInventoryQuery ApplyPriceRangeFilter(IInventoryQuery query)
    {
        decimal min, max;

        do
        {
            min = _reader.ReadDecimal("* Price minimum (default $0): ", 0m);
            max = _reader.ReadDecimal("* Price maximum (default $1000): ", 1000m);

            if (min > max)
                _reader.WriteMessage("\n Minimum price cannot exceed maximum. Please re-enter.\n");
        } while (min > max);

        return new PriceRangeFilterDecorator(query, min, max);
    }

    private IInventoryQuery ApplyStarRatingFilter(IInventoryQuery query)
    {
        int min, max;

        do
        {
            min = _reader.ReadInt("* Star rating minimum (1-5, default 3): ", 3, 1, 5);
            max = _reader.ReadInt("* Star rating maximum (1-5, default 5): ", 5, 1, 5);

            if (min > max) _reader.WriteMessage("\nMinimum star rating cannot exceed maximum. Please re-enter\n");
        } while (min > max);

        return new StarRatingRangeFilterDecorator(query, new StarRating(min), new StarRating(max));
    }

    private IInventoryQuery ApplyPriceSort(IInventoryQuery query)
    {
        var choice = _reader.ReadChoice("* Sort by Price(A/D, default A): ", "A");
        var direction = choice == "D" ? SortDirection.Descending : SortDirection.Ascending;
        return new SortByPriceDecorator(query, direction);
    }

    private IInventoryQuery ApplyStarRatingSort(IInventoryQuery query)
    {
        var choice = _reader.ReadChoice("* Sort by Star Rating (A/D, default D): ", "D");
        var direction = choice == "A" ? SortDirection.Ascending : SortDirection.Descending;
        return new SortByStarRatingDecorator(query, direction);
    }
}