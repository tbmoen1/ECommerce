using PaymentService.Application;
using PaymentService.Infrastructure.HttpClients.Payments;
using PaymentService.Infrastructure.Payments.Stripe.Models;

namespace PaymentService.Infrastructure.Payments.Stripe;

public class StripePaymentProvider(IHttpClientFactory clientFactory) : IPaymentProvider
{
    public async Task<CheckoutSessionDto?> CreateCheckoutSessionAsync(CreateCheckoutSessionDto dto)
    {
        var client = clientFactory.CreateClient(nameof(StripeHttpClient));
        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["line_items[0][price]"] = dto.PriceId,
            ["line_items[0][quantity]"] = dto.Quantity.ToString(),
            ["ui_mode"] = "elements",
            ["return_url"] = "http://localhost",
            ["mode"] = "payment"
        });
        var request = await client.PostAsync("v1/checkout/sessions", content);
        request.EnsureSuccessStatusCode();
        return await request.Content.ReadFromJsonAsync<CheckoutSessionDto>();
    }
}