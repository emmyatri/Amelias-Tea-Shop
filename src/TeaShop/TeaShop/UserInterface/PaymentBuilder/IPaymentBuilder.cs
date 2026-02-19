using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface;

public interface IPaymentBuilder
{
    IPaymentStrategy Build();
}