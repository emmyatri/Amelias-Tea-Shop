namespace TeaShop.Domain.Inventory;

/// <summary>
///     Represents a 1-5 star rating with invariant enforcement.
/// </summary>
public record StarRating(int StarValue) : IComparable<StarRating>
{
    public int StarValue { get; } = StarValue is < 1 or > 5
        ? throw new ArgumentOutOfRangeException(nameof(StarValue), "Star rating must be between 1 and 5.")
        : StarValue;

    /// <summary>
    ///     Compares two star ratings by their numeric value.
    /// </summary>
    public int CompareTo(StarRating? other)
    {
        return other is null ? 1 : StarValue.CompareTo(other.StarValue);
    }


    /// <summary>
    ///     Returns a string of ★ characters matching the rating value.
    /// </summary>
    public override string ToString()
    {
        return new string('★', StarValue);
    }
}