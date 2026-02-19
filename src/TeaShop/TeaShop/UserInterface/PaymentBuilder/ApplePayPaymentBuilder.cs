using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface;

public class ApplePayPaymentBuilder(TextReader input, TextWriter output) : IPaymentBuilder
{
    private readonly TextReader _input = input;
    private readonly TextWriter _output = output;


    public IPaymentStrategy Build()
    {
        //return null;
    };
}