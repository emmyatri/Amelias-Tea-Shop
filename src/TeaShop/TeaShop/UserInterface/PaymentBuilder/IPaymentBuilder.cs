using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;

public interface IPaymentBuilder
{
    string Name { get; }
    IPaymentStrategy Build(PurchaseDetails purchase);
    
}