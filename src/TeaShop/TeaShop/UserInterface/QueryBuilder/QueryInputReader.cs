using System;

namespace TeaShop.UserInterface.QueryBuilder;

public class QueryInputReader(TextReader reader, TextWriter writer)
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
        
        _writer.Write(prompt);
        var input = _reader.ReadLine()?.Trim() ?? "";
        _writer.WriteLine("-----");
        return input == "" ? defaultValue
            : decimal.TryParse(input, out var val) ? val : defaultValue;
        
    }

    public int ReadInt(string prompt, int defaultValue)
    {
        
        _writer.Write(prompt);
        var input = _reader.ReadLine()?.Trim() ?? "";
        _writer.WriteLine("-----");
        return input == "" ? defaultValue
            : int.TryParse(input, out var val) ? val : defaultValue;
        
    }

    public string ReadChoice(string prompt, string defaultValue)
    {
        _writer.Write(prompt);
        var input = _reader.ReadLine()?.Trim().ToUpper() ?? "";
        _writer.WriteLine("-----");
        return input == "" ? defaultValue : input;

    }
    
}