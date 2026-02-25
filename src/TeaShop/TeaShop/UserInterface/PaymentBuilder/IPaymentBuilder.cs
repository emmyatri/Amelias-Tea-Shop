using TeaShop.Domain.Payment;

namespace TeaShop.UserInterface.PaymentBuilder;


/// <summary>
///     Bridges the UI and domain payment layers. Collects payment-specific
///     input from the user and constructs the corresponding payment strategy.
/// </summary>
public interface IPaymentBuilder
{
    /// <summary>
    ///     Display name shown in the payment method menu.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    ///     Prompts the user for payment details and returns a ready-to-execute strategy.
    /// </summary>
    IPaymentStrategy Build(PurchaseDetails purchase);
    
}