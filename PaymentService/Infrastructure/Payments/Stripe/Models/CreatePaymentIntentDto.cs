namespace PaymentService.Infrastructure.Payments.Stripe.Models;

public class CreatePaymentIntentDto(int amount, string currency)
{
    public int Amount { get; set; } = amount;
    public string Currency { get; set; } = currency;
}