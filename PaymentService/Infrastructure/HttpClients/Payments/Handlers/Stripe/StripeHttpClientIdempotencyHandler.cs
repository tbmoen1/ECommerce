namespace PaymentService.Infrastructure.HttpClients.Payments.Handlers.Stripe;

public class StripeHttpClientIdempotencyHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Stripe ignores idempotency keys on GET and DELETE, and these methods are idempotent by definition anyway
        if (request.Method == HttpMethod.Get
            || request.Method == HttpMethod.Delete
            || request.Method == HttpMethod.Head
            || request.Method == HttpMethod.Options)
        {
            return base.SendAsync(request, cancellationToken);
        }

        var idempotencyKey = StripeIdempotency.GetOrCreateIdempotencyKey(request);

        request.Headers.Remove(StripeIdempotency.HeaderName);
        request.Headers.TryAddWithoutValidation(StripeIdempotency.HeaderName, idempotencyKey);

        return base.SendAsync(request, cancellationToken);
    }
}
