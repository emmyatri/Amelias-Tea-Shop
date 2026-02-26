using TeaShop.Domain.Inventory;
using TeaShop.UserInterface;
using TeaShop.UserInterface.PaymentBuilder;
using TeaShop.UserInterface.PurchaseBuilder;
using TeaShop.UserInterface.QueryBuilder;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var repository = new InventoryRepository();
var input = new UserPrompt(Console.In, Console.Out);
var queryBuilder = new InventoryQueryBuilder(input, repository);
var outputWriter = new InventoryQueryOutputWriter(Console.Out);
var paymentMethods = new PaymentBuilderListFactory().Create(input, Console.Out);
var paymentHandler = new PaymentHandler(input, paymentMethods, Console.Out);
var purchaseHandler = new PurchaseHandler(input, paymentHandler, repository, Console.Out);
var app = new Application(input, queryBuilder, outputWriter, purchaseHandler, Console.Out);

app.Run();