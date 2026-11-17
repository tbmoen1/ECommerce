namespace PaymentService.Infrastructure.Prices.Stripe.Models;

public class CreatePriceDto(string currency, string productId, int unitAmount)
{
    public string Currency { get; set; } = currency;
    public string ProductId { get; set; } = productId;
    public int UnitAmount { get; set; } = unitAmount;
}
