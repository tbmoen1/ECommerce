using PaymentService.Application;
using PaymentService.Infrastructure.HttpClients.Payments;
using PaymentService.Infrastructure.Prices.Stripe.Models;

namespace PaymentService.Infrastructure.Prices.Stripe;

public class StripePriceProvider(IHttpClientFactory clientFactory) : IPriceProvider
{
    public async Task<PriceDto?> CreatePrice(CreatePriceDto dto)
    {
        var client = clientFactory.CreateClient(nameof(StripeHttpClient));
        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["currency"] = dto.Currency,
            ["product"] = dto.ProductId,
            ["unit_amount"] = dto.UnitAmount.ToString(),
        });
        var request = await client.PostAsync("v1/prices", content);
        request.EnsureSuccessStatusCode();
        return await request.Content.ReadFromJsonAsync<PriceDto>();
    }
}
