using TeaShop.Domain.Inventory;

namespace TeaShop.Domain.InventoryQuery.Filters;

/// <summary>
///     Filters inventory items within a minimum and maximum price range (inclusive).
/// </summary>
public sealed class PriceRangeFilterDecorator : InventoryQueryDecoratorBase
{
    private readonly decimal _searchPriceMin;
    private readonly decimal _searchPriceMax;

    public PriceRangeFilterDecorator(IInventoryQuery inner, decimal searchPriceMin, decimal searchPriceMax)
        : base(inner)
    {
        if (searchPriceMin < 0)
            throw new ArgumentOutOfRangeException(nameof(searchPriceMin), "Price must be non-negative.");
        if (searchPriceMax < 0)
            throw new ArgumentOutOfRangeException(nameof(searchPriceMax), "Price must be non-negative.");
        if (searchPriceMin > searchPriceMax)
            throw new ArgumentException("Minimum price cannot exceed maximum price.");

        _searchPriceMin = searchPriceMin;
        _searchPriceMax = searchPriceMax;
    }
   

    protected override FilterDescription? AppliedDescription
        => new(FilterType.Filter, $"Price range between {_searchPriceMin:C} and {_searchPriceMax:C}");

    protected override IReadOnlyList<InventoryItem> Decorate(IReadOnlyList<InventoryItem> items)
    {
        return items.Where(item =>
            item.Price >= _searchPriceMin && item.Price <= _searchPriceMax).ToList().AsReadOnly();
    }
}