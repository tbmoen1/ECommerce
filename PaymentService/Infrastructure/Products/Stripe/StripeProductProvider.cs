using PaymentService.Application;
using PaymentService.Infrastructure.HttpClients.Payments;
using PaymentService.Infrastructure.Products.Stripe.Models;

namespace PaymentService.Infrastructure.Products.Stripe;

public class StripeProductProvider(IHttpClientFactory clientFactory) : IProductProvider
{
    public async Task<ProductDto?> CreateProduct(CreateProductDto dto)
    {
        var client = clientFactory.CreateClient(nameof(StripeHttpClient));
        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["name"] = dto.Name,
            // unit_amount is the lowest unit of the currency specified for the price. E.g. 1000 units in usd is $10
            ["default_price_data[unit_amount]"] = dto.Amount.ToString(),
            ["default_price_data[currency]"] = dto.Currency
        });
        var request = await client.PostAsync("v1/products", content);
        request.EnsureSuccessStatusCode();
        return await request.Content.ReadFromJsonAsync<ProductDto>();
    }
}   