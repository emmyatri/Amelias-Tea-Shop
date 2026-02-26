namespace TeaShop.UserInterface;

public interface IUserPrompt
{
    string ReadString(string prompt);
    decimal ReadDecimal(string prompt, decimal defaultValue);
    int ReadInt(string prompt, int? defaultValue, int min = int.MinValue, int max = int.MaxValue);
    string ReadChoice(string prompt, string defaultValue);
    void ShowError(string message);
}