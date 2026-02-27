using TeaShop.Domain.Inventory;
using TeaShop.Domain.Payment;

namespace TeaShop.UnitTest.Domain.Payment;

public class PaymentStrategyTests
{
    private readonly InventoryItem _testItem =
        new(Guid.NewGuid(), "Green Tea", 15.00m, 50, new StarRating(4));
    
    //CreditCardStrat
    
    [Fact]
    public void CreditCard_Checkout_ReturnsCorrectResult()
    {
        var strategy = new CreditCardStrategy("1234567890123456");

        var result = strategy.Checkout(_testItem, 2);

        Assert.Equal(30.00m, result.Total);
        Assert.Equal("Credit Card", result.PaymentMethod);
        Assert.Equal("3456", result.MaskedIdentifier);
    }
    
    [Fact]
    public void CreditCard_TooShort_Throws()
    {
        Assert.Throws<ArgumentException>(() => new CreditCardStrategy("123"));
    }
    
    //ApplePayStrat
    
    [Fact]
    public void ApplePay_Checkout_ReturnsCorrectResult()
    {
        var strategy = new ApplePayStrategy("1234567890");

        var result = strategy.Checkout(_testItem, 3);

        Assert.Equal(45.00m, result.Total);
        Assert.Equal("ApplePay", result.PaymentMethod);
        Assert.Equal("7890", result.MaskedIdentifier);
    }

    [Fact]
    public void ApplePay_WrongLength_Throws()
    {
        Assert.Throws<ArgumentException>(() => new ApplePayStrategy("12345"));
    }
    
    //CryptoWalletStrat
    
    [Fact]
    public void Crypto_Checkout_ReturnsCorrectResult()
    {
        var strategy = new CryptoCurrencyStrategy("ABCDEF123456");

        var result = strategy.Checkout(_testItem, 4);

        Assert.Equal(60.00m, result.Total);
        Assert.Equal("CryptoCurrency", result.PaymentMethod);
        Assert.Equal("123456", result.MaskedIdentifier);
    }

    [Fact]
    public void Crypto_TooShort_Throws()
    {
        Assert.Throws<ArgumentException>(() => new CryptoCurrencyStrategy("ABC"));
    }
    
    [Fact]
    public void Checkout_ReturnsStructuredData_NotFormattedStrings()
    {
        var strategy = new CreditCardStrategy("1234567890123456");

        var result = strategy.Checkout(_testItem, 2);

        // PaymentResult returns raw data — the UI decides formatting
        Assert.DoesNotContain("$", result.PaymentMethod);
        Assert.DoesNotContain("ending in", result.MaskedIdentifier);
        Assert.DoesNotContain("Paid", result.PaymentMethod);
    }
    
}