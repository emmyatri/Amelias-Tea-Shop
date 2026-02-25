using TeaShop.Domain.InventoryQuery;

namespace TeaShop.UserInterface.QueryBuilder;

public record QueryContext(IInventoryQuery Query, List<string> AppliedFilters);