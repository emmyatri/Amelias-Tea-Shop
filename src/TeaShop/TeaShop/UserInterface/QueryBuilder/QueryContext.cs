using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;


/// <summary>
///     Carries the current query and accumulated filter descriptions
///     through the builder pipeline.
/// </summary>
public record QueryContext(IInventoryQuery Query, List<string> AppliedFilters);