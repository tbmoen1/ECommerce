using PaymentService.Infrastructure.HttpClients.Payments.Handlers.Stripe;

namespace PaymentService.Infrastructure.HttpClients.Payments;

public static class StripeHttpClient
{
    public static void AddStripeHttpClient(this IServiceCollection services)
    {
        services.AddTransient<StripeHttpClientAuthenticationHandler>();
        services.AddTransient<StripeHttpClientIdempotencyHandler>();

        services.AddHttpClient(nameof(StripeHttpClient), 
                client => client.BaseAddress = new Uri("https://api.stripe.com/"))
            .AddHttpMessageHandler<StripeHttpClientAuthenticationHandler>()
            .AddHttpMessageHandler<StripeHttpClientIdempotencyHandler>();
    }
}