using TeaShop.Domain.Inventory;
using TeaShop.UserInterface.PaymentBuilder;
using TeaShop.UserInterface.QueryBuilder;

namespace TeaShop.UserInterface;


/// <summary>
///     Top-level orchestrator for the tea shop. Coordinates the
///     search-display-purchase loop without owning any business logic.
/// </summary>
public sealed class Application
{
    private readonly IUserPrompt _reader;
    private readonly TextWriter _writer;
    private readonly InventoryQueryBuilder _queryBuilder;
    private readonly InventoryQueryOutputWriter _outputWriter;
    private readonly PurchaseHandler _purchaseHandler;

    public Application(TextReader reader, TextWriter writer)
    {
        ArgumentNullException.ThrowIfNull(reader);
        ArgumentNullException.ThrowIfNull(writer);

        _writer = writer;
        _reader = new UserPrompt(reader, writer);
        
        var repository = new InventoryRepository();
        var paymentMethods = PaymentBuilderListFactory.Create(_reader);
        
        _queryBuilder = new InventoryQueryBuilder(_reader, repository);
        _outputWriter = new InventoryQueryOutputWriter(_writer);
        _purchaseHandler = new PurchaseHandler(_reader, _writer, repository, paymentMethods);
    }
    
    public void Run()
    {
        _writer.WriteLine("===WELCOME TO AMELIA'S TEA SHOP===");
        _writer.WriteLine();
        _writer.WriteLine("Complete the prompts to search our selection of fine teas.");
        _writer.WriteLine();

        var searchAgain = true;

        while (searchAgain)
        {
            var query = _queryBuilder.Build();
            var output = InventoryQueryOutput.From(query);
            _outputWriter.Write(output);

            if (output.Items.Count > 0)
                _purchaseHandler.Handle(output);

            searchAgain = _reader.ReadChoice("\nSearch for more tea? (Y/N, default Y): ", "Y") != "N";
        }
    }
    


}

