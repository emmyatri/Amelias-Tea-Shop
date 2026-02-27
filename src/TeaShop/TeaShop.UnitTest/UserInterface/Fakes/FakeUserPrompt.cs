using TeaShop.UserInterface;

namespace TeaShop.UnitTest.UserInterface.Fakes;

public class FakeUserPrompt :IUserPrompt
{
    private readonly Queue<string> _strings = new();
    private readonly Queue<decimal> _decimals = new();
    private readonly Queue<int> _ints = new();
    private readonly Queue<string> _choices = new();

    public FakeUserPrompt WithString(string response) { _strings.Enqueue(response); return this; }
    public FakeUserPrompt WithDecimal(decimal response) { _decimals.Enqueue(response); return this; }
    public FakeUserPrompt WithInt(int response) { _ints.Enqueue(response); return this; }
    public FakeUserPrompt WithChoice(string response) { _choices.Enqueue(response); return this; }

    public string ReadString(string prompt) => _strings.Count > 0 ? _strings.Dequeue() : "";
    public decimal ReadDecimal(string prompt, decimal defaultValue) => _decimals.Count > 0 ? _decimals.Dequeue() : defaultValue;
    public int ReadInt(string prompt, int? defaultValue, int min = int.MinValue, int max = int.MaxValue) => _ints.Count > 0 ? _ints.Dequeue() : defaultValue ?? min;
    public string ReadChoice(string prompt, string defaultValue) => _choices.Count > 0 ? _choices.Dequeue() : defaultValue;
    public void ShowError(string message) { }
    
}