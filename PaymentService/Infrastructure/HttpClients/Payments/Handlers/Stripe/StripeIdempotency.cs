namespace PaymentService.Infrastructure.HttpClients.Payments.Handlers.Stripe;

public static class StripeIdempotency
{
    internal const string HeaderName = "Idempotency-Key";

    private static readonly HttpRequestOptionsKey<string> OptionKey = new("StripeIdempotencyKey");

    internal static string GetOrCreateIdempotencyKey(HttpRequestMessage request)
    {
        if (request.Headers.TryGetValues(HeaderName, out var values))
        {
            var headerKey = values.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(headerKey))
            {
                return headerKey;
            }
        }

        if (request.Options.TryGetValue(OptionKey, out var key))
        {
            return key;
        }

        key = Guid.NewGuid().ToString();
        request.Options.Set(OptionKey, key);
        return key;
    }
}
