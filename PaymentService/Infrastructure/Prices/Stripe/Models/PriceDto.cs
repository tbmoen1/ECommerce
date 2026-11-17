using System.Text.Json.Serialization;

namespace PaymentService.Infrastructure.Prices.Stripe.Models;

public class PriceDto
{
    public class RecurringDto
    {
        [JsonPropertyName("interval")]
        public string Interval { get; set; }

        [JsonPropertyName("interval_count")]
        public int? IntervalCount { get; set; }

        [JsonPropertyName("trial_period_days")]
        public object TrialPeriodDays { get; set; }

        [JsonPropertyName("usage_type")]
        public string UsageType { get; set; }
    }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("object")]
    public string Object { get; set; }

    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    [JsonPropertyName("billing_scheme")]
    public string BillingScheme { get; set; }

    [JsonPropertyName("created")]
    public int? Created { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("custom_unit_amount")]
    public object CustomUnitAmount { get; set; }

    [JsonPropertyName("livemode")]
    public bool? Livemode { get; set; }

    [JsonPropertyName("lookup_key")]
    public object LookupKey { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, string> Metadata { get; set; }

    [JsonPropertyName("nickname")]
    public object Nickname { get; set; }

    [JsonPropertyName("product")]
    public string Product { get; set; }

    [JsonPropertyName("recurring")]
    public RecurringDto Recurring { get; set; }

    [JsonPropertyName("tax_behavior")]
    public string TaxBehavior { get; set; }

    [JsonPropertyName("tiers_mode")]
    public object TiersMode { get; set; }

    [JsonPropertyName("transform_quantity")]
    public object TransformQuantity { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("unit_amount")]
    public int? UnitAmount { get; set; }

    [JsonPropertyName("unit_amount_decimal")]
    public string UnitAmountDecimal { get; set; }
}
