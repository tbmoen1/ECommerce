namespace PaymentService.Infrastructure.Products.Stripe.Models;

public class CreateProductDto(string name, int amount, string currency)
{
    public string Name { get; set; } = name;
    public int Amount { get; set; } = amount;
    public string Currency { get; set; } = currency;
}
