namespace TeaShop.Domain.Inventory;


/// <summary>
/// Represents a 1-5 star rating with invariant enforcement.
/// </summary>
public class StarRating : IComparable<StarRating>
{
    
    public int StarValue { get; }

    
    public StarRating(int value)
    {
        
        if (value is < 1 or > 5)
            throw new ArgumentOutOfRangeException(
                nameof(value), "Star rating must be between 1 and 5.");
        
        StarValue =  value;
        
    }

    
    public int CompareTo(StarRating? other) => 
        other is null ? 1 : StarValue.CompareTo(other.StarValue);

    
    public override bool Equals(object? obj) =>
        obj is StarRating other && StarValue == other.StarValue;
    
    
    public override int GetHashCode() =>
        StarValue.GetHashCode();
    
    
    public override string ToString() =>
        new string('★', StarValue);
    
}