## Running the Application

### Console

```bash
cd src/TeaShop
dotnet run --project TeaShop/TeaShop.csproj
```
### Docker

```bash
cd src/TeaShop
docker build -t teashop .
docker run -it teashop
```
The ``-it`` flag is required due to this being an interactive console application.

### Running Tests

```bash
cd src/TeaShop
dotnet test
```

The test suite includes 41 tests across 7 files covering domain logic (StarRating invariants, InventoryItem behavior, InventoryRepository mutations), decorator chain composition (all filters, sorts, and combined chains), payment strategies (structured results, validation, masked identifiers), and a UI integration test using ``FakeUserPrompt`` to exercise ``InventoryQueryBuilder`` without touching the console.
