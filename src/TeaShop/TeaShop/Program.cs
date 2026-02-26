using TeaShop.UserInterface;

namespace TeaShop;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        
        var application = new Application(Console.In, Console.Out);
        application.Run();
        
    }
}