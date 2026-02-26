namespace TeaShop.UserInterface.QueryBuilder;


/// <summary>
///     Centralizes all console input/output operations for testability
///     and consistent prompt formatting.
/// </summary>
public sealed class UserPrompt(TextReader reader, TextWriter writer)
{
    private readonly TextReader _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));


    public string ReadString(string prompt)
    {
        _writer.Write(prompt);
        var input = _reader.ReadLine()?.Trim() ?? "";
        _writer.WriteLine("\n-----");
        return input;
    }

    public decimal ReadDecimal(string prompt, decimal defaultValue)
    {
        while (true)
        {
            _writer.Write(prompt);
            var input = _reader.ReadLine()?.Trim() ?? "";
            _writer.WriteLine("-----");

            if (string.IsNullOrWhiteSpace(input)) return defaultValue;

            var cleaned = input.Replace("$", "");
            if (decimal.TryParse(cleaned, out var val) && val >= 0)
                return val;

            _writer.WriteLine("\nInvalid. Please enter a valid, non-negative number\n");
        }
    }

    public int ReadInt(string prompt, int defaultValue, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            _writer.Write(prompt);
            var input = _reader.ReadLine()?.Trim() ?? "";
            _writer.WriteLine("-----");

            if (string.IsNullOrWhiteSpace(input)) return defaultValue;

            if (int.TryParse(input, out var val) && val >= min && val <= max)
                return val;

            if (min != int.MinValue && max != int.MaxValue)
                _writer.WriteLine($"\nPlease enter a whole number between {min} and {max}.\n");
            else
                _writer.WriteLine("\nPlease enter a valid number.\n");
        }
    }

    public string ReadChoice(string prompt, string defaultValue)
    {
        _writer.Write(prompt);
        var input = _reader.ReadLine()?.Trim().ToUpper() ?? "";
        _writer.WriteLine("-----");
        return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
    }

    public void WriteMessage(string message)
    {
        _writer.WriteLine(message);
    }
}