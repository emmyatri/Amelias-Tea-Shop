using TeaShop.UserInterface.QueryBuilder;
using TeaShop.UserInterface.PurchaseBuilder;

namespace TeaShop.UserInterface;


/// <summary>
///     Top-level orchestrator for the tea shop. Coordinates the
///     search-display-purchase loop without owning any business logic.
/// </summary>
public sealed class Application(
    UserPrompt reader,
    InventoryQueryBuilder queryBuilder,
    InventoryQueryOutputWriter outputWriter,
    PurchaseHandler purchaseHandler,
    TextWriter writer)

{
    private readonly UserPrompt _reader = reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly InventoryQueryBuilder _queryBuilder =
        queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));

    private readonly InventoryQueryOutputWriter _outputWriter =
        outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));

    private readonly PurchaseHandler _purchaseHandler =
        purchaseHandler ?? throw new ArgumentNullException(nameof(purchaseHandler));

    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));


    public void Run()
    {
        _writer.WriteLine("===WELCOME TO AMELIA'S TEA SHOP===");
        _writer.WriteLine();
        _writer.WriteLine("Complete the prompts to search our selection of fine teas.");
        _writer.WriteLine();

        var searchAgain = true;

        while (searchAgain)
        {
            var output = _queryBuilder.Build();
            _outputWriter.Write(output);

            if (output.Items.Count > 0)
                _purchaseHandler.TryPurchase(output);

            searchAgain = _reader.ReadChoice("Search for more tea? (Y/N, default Y)", "Y") != "N";
        }
    }
}