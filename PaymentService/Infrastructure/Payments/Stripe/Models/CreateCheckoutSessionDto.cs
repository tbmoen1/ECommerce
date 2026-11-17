namespace PaymentService.Infrastructure.Payments.Stripe.Models;

public class CreateCheckoutSessionDto(string priceId, int quantity)
{
    public string PriceId { get; set; } = priceId;
    public int Quantity { get; set; } = quantity;
}