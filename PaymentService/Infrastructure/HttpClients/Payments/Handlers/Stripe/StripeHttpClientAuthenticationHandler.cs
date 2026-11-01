using System.Net.Http.Headers;

namespace PaymentService.Infrastructure.HttpClients.Payments.Handlers.Stripe;

public class StripeHttpClientAuthenticationHandler(IConfiguration configuration) : DelegatingHandler
{
    private string? ApiKey { get; } = configuration["Stripe:ApiKey"] 
                                      ?? throw new ArgumentException("Stripe API Key not found");
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
        return await base.SendAsync(request, cancellationToken);
    }
}