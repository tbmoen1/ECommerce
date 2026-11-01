using PaymentService.Application;
using PaymentService.Infrastructure.HttpClients.Payments;
using PaymentService.Infrastructure.Payments.Stripe.Models;

namespace PaymentService.Infrastructure.Payments.Stripe;

public class StripePaymentProvider(IHttpClientFactory clientFactory) : IPaymentProvider
{
    public async Task<PaymentIntentDto?> CreatePaymentIntentAsync(CreatePaymentIntentDto dto)
    {
        var client = clientFactory.CreateClient(nameof(StripeHttpClient));
        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["amount"] = dto.Amount.ToString(),
            ["currency"] = dto.Currency,
        });
        
        var request = await client.PostAsync("v1/payment_intents", content);  
        request.EnsureSuccessStatusCode();
        return await request.Content.ReadFromJsonAsync<PaymentIntentDto>();
    }
}