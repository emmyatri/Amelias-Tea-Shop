namespace TeaShop.Domain.Payment;

public record PaymentResult(decimal Total, string PaymentMethod, string MaskedIdentifier);