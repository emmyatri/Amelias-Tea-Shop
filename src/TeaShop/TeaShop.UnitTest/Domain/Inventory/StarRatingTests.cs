using TeaShop.Domain.Inventory;


namespace TeaShop.UnitTest.Domain.Inventory;

public class StarRatingTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void Constructor_ValidRating_SetsStarValue(int value)
    {
        var rating = new StarRating(value);

        Assert.Equal(value, rating.StarValue);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    [InlineData(-1)]
    public void Constructor_InvalidRating_ThrowsArgumentOutOfRange(int value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new StarRating(value));
    }

    [Fact]
    public void CompareTo_HigherRating_ReturnsNegative()
    {
        var low = new StarRating(2);
        var high = new StarRating(4);

        Assert.True(low.CompareTo(high) < 0);
    }

    [Fact]
    public void CompareTo_LowerRating_ReturnsPositive()
    {
        var high = new StarRating(4);
        var low = new StarRating(2);

        Assert.True(high.CompareTo(low) > 0);
    }

    [Fact]
    public void CompareTo_EqualRating_ReturnsZero()
    {
        var a = new StarRating(3);
        var b = new StarRating(3);

        Assert.Equal(0, a.CompareTo(b));
    }

    [Fact]
    public void CompareTo_Null_ReturnsPositive()
    {
        var rating = new StarRating(1);

        Assert.True(rating.CompareTo(null) > 0);
    }

    [Fact]
    public void ToString_ReturnsStarCharacters()
    {
        var rating = new StarRating(4);

        Assert.Equal("★★★★", rating.ToString());
    }

    [Fact]
    public void Equality_SameValue_AreEqual()
    {
        var a = new StarRating(3);
        var b = new StarRating(3);

        Assert.Equal(a, b);
    }

    [Fact]
    public void Equality_DifferentValue_AreNotEqual()
    {
        var a = new StarRating(2);
        var b = new StarRating(4);

        Assert.NotEqual(a, b);
    }
}