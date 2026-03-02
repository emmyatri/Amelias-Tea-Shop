# Amelia's Tea Shop

A console-based tea shop application built in C# (.NET 10) that allows users to search, filter, sort, and purchase from an inventory of authentic teas. Built to demonstrate intentional use of object-oriented design patterns and SOLID principles.


## How It Works

The app walks you through a search-and-purchase loop. You set filters (name, availability, price range, star rating), choose sort directions, and browse the results. If something catches your eye, select it by number, pick a quantity, choose a payment method, and check out. The inventory updates in real time, and you can search again as many times as you'd like.

## Design Highlights

#### PaymentResult Over void for Zero Information Leakage

The given solution had payment strategies write directly to a ``TextWriter``

```csharp 
// Professor's approach
void Checkout(InventoryItem item, int quantity, TextWriter output)
```

My strategy returns a structured ``PaymentResult`` record instead.

```csharp
// My approach
PaymentResult Checkout(InventoryItem item, int quantity)
// where PaymentResult is: record PaymentResult(decimal Total, string PaymentMethod, string MaskedIdentifier)
```
The domain layer returns data, and the UI Interface decides how to display it. This means you could swap the console for a web UI without touching a single domain class. The domain has zero knowledge of formatting, currency symbols, or display strings.

#### IUserPrompt - A Deep Module for Console I/O

Instead of threading raw ``TextReader``/``TextWriter`` through every class that needs user input, I extracted an ``IUserPrompt`` interface with five methods: ``ReadString``, ``ReadDecimal``, ``ReadInt``, ``ReadChoice``, and ``ShowError``. The concrete ``UserPrompt`` class hides retry loops, input validation, default handling, and error formatting behind these simple calls.

The payoff is testability. In unit tests, I swap in a ``FakeUserPrompt`` that returns pre-configured responses from queues. The test reads like a specification: 

```csharp
var prompt = new FakeUserPrompt()
    .WithString("Oolong")
    .WithChoice("Y")
    .WithDecimal(0m)
    .WithDecimal(1000m)
    .WithInt(3)
    .WithInt(5)
    .WithChoice("A")
    .WithChoice("D");
```
    
#### Template Method in PaymentStrategyBase

The base class owns the checkout algorithm by computing total via ``item.PriceFor(quantity)``, gathering payment details, and returning a ``PaymentResult``. Subclasses implement only two one-liners: ``GetPaymentMethod()`` and ``GetMaskedIdentifier()``. Adding a new payment method means writing a class with two methods, not reimplementing the entire checkout flow.

#### Masked Identifiers for Designed Privacy

Each payment strategy masks sensitive data structurally. ``CreditCardStrategy.GetMaskedIdentifier()`` returns ``_creditCardNumber[^4..]``. This method cannot return the full number. The UI displays ``ending in [3456]`` without ever having access to the complete card number.

#### PriceFor on the Entity

Pricing logic lives on ``InventoryItem.PriceFor(quantity)`` with a guard clause that rejects non-positive quantities. This gives a single source of truth so that if pricing rules ever change (bulk discounts, rounding), there's only one place to update instead of hunting for ``price * quantity`` scattered across the codebase.

#### PurchaseHandler — SRP in Application

``Application.Run()`` is 15 lines of pure orchestration. Purchase logic (item selection, quantity validation, payment selection, checkout) lives in ``PurchaseHandler``. Each class has one reason to change.

#### StarRating Implements IComparable<StarRating>

Comparison semantics are encapsulated in the type. Sort decorators just call ``.OrderBy(item => item.StarRating)`` and LINQ picks up ``IComparable<T>`` automatically. If the rating system changes (half-stars, weighted ratings), the sort decorators don't need to be touched.

#### Dependency Injection Throughout

Every class receives its dependencies through the constructor. ``InventoryQueryBuilder`` takes ``IUserPrompt`` and ``InventoryRepository``. ``PurchaseHandler`` takes ``IUserPrompt``, ``TextWriter``, ``InventoryRepository``, and ``IReadOnlyList<IPaymentBuilder>``. Application is the composition root and wires everything together.

## Patterns Used

**Decorator Pattern:**  Inventory queries are composed as a chain of decorators. Each filter and sort wraps an ``IInventoryQuery``, and calls ``Execute()`` once to run the entire pipeline. New filters or sorts are added by creating a new class meaning no existing code needs to change (OCP).

**Strategy Pattern:** Payment processing is polymorphic through ``IPaymentStrategy``. The checkout logic calls ``strategy.Checkout()`` without knowing the concrete type. No if/else or switch on payment type anywhere in the codebase.

**Template Method Pattern:** Both ``PaymentStrategyBase`` and ``InventoryQueryDecoratorBase`` use template methods. The base class defines the algorithm skeleton and subclasses fill in the specific behavior.

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

## Project Structure

```
Amelias-Tea-Shop/
├── README.md
├── screenshot.png
├── tea-shop-class-diagram.md
├── .gitignore
└── src/
    └── TeaShop/
        ├── TeaShop.sln
        ├── Dockerfile
        ├── .dockerignore
        ├── TeaShop/
        │   ├── TeaShop.csproj
        │   ├── Program.cs
        │   ├── Domain/
        │   │   ├── Inventory/
        │   │   │   ├── InventoryItem.cs
        │   │   │   ├── InventoryRepository.cs
        │   │   │   └── StarRating.cs
        │   │   ├── InventoryQuery/
        │   │   │   ├── IInventoryQuery.cs
        │   │   │   ├── AllInventoryQuery.cs
        │   │   │   ├── InventoryQueryDecoratorBase.cs
        │   │   │   ├── QueriedInventoryItem.cs
        │   │   │   ├── Filters/
        │   │   │   │   ├── AvailabilityFilterDecorator.cs
        │   │   │   │   ├── NameContainsFilterDecorator.cs
        │   │   │   │   ├── PriceRangeFilterDecorator.cs
        │   │   │   │   └── StarRatingRangeFilterDecorator.cs
        │   │   │   └── Sorts/
        │   │   │       ├── SortByPriceDecorator.cs
        │   │   │       ├── SortByStarRatingDecorator.cs
        │   │   │       └── SortDirection.cs
        │   │   └── Payment/
        │   │       ├── IPaymentStrategy.cs
        │   │       ├── PaymentStrategyBase.cs
        │   │       ├── PaymentResult.cs
        │   │       ├── CreditCardStrategy.cs
        │   │       ├── ApplePayStrategy.cs
        │   │       └── CryptoCurrencyStrategy.cs
        │   └── UserInterface/
        │       ├── Application.cs
        │       ├── IUserPrompt.cs
        │       ├── UserPrompt.cs
        │       ├── PurchaseHandler.cs
        │       ├── QueryBuilder/
        │       │   ├── InventoryQueryBuilder.cs
        │       │   ├── InventoryQueryOutput.cs
        │       │   └── InventoryQueryOutputWriter.cs
        │       └── PaymentBuilder/
        │           ├── IPaymentBuilder.cs
        │           ├── PaymentBuilderListFactory.cs
        │           ├── CreditCardPaymentBuilder.cs
        │           ├── ApplePayPaymentBuilder.cs
        │           └── CryptoCurrencyPaymentBuilder.cs
        └── TeaShop.UnitTest/
            ├── TeaShop.UnitTest.csproj
            ├── Domain/
            │   ├── Inventory/
            │   │   ├── StarRatingTests.cs
            │   │   ├── InventoryItemTests.cs
            │   │   └── InventoryRepositoryTests.cs
            │   ├── InventoryQuery/
            │   │   └── QueryTests.cs
            │   └── Payment/
            │       └── PaymentStrategyTests.cs
            └── UserInterface/
                ├── Fakes/
                │   └── FakeUserPrompt.cs
                └── InventoryQueryBuilderTests.cs
```

## UML Class Diagram

See [Class Diagram](tea-shop-class-diagram.md) for full UML

## Built With

- C# / .NET 10
- xUnit (unit testing)
- Docker
