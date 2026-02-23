namespace TeaShop.Domain.Payment;

public interface IPaymentStrategy
{
    /// <summary>
    ///     Executes the simulated checkout and returns a confirmation message.
    /// </summary>
    string Checkout();
}