namespace TeaShop.Domain.Inventory;

/// <summary>
///     Represents a 1-5 star rating with invariant enforcement.
/// </summary>
public sealed class StarRating : IComparable<StarRating>
{
    public StarRating(int value)
    {
        if (value is < 1 or > 5)
            throw new ArgumentOutOfRangeException(
                nameof(value), "Star rating must be between 1 and 5.");

        StarValue = value;
    }

    public int StarValue { get; }


    /// <summary>
    ///     Compares two star ratings by their numeric value.
    /// </summary>
    public int CompareTo(StarRating? other)
    {
        return other is null ? 1 : StarValue.CompareTo(other.StarValue);
    }


    public override bool Equals(object? obj)
    {
        return obj is StarRating other && StarValue == other.StarValue;
    }


    public override int GetHashCode()
    {
        return StarValue.GetHashCode();
    }


    /// <summary>
    ///     Returns a string of ★ characters matching the rating value.
    /// </summary>
    public override string ToString()
    {
        return new string('★', StarValue);
    }
}