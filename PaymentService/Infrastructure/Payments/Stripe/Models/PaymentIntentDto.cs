using System.Text.Json.Serialization;

namespace PaymentService.Infrastructure.Payments.Stripe.Models;

// Generated with https://json2csharp.com/
public class PaymentIntentDto
{ 
    public class AutomaticPaymentMethods
    {
        [JsonPropertyName("allow_redirects")]
        public string AllowRedirects { get; set; }

        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }
    }

    public class Card
    {
        [JsonPropertyName("installments")]
        public object Installments { get; set; }

        [JsonPropertyName("mandate_options")]
        public object MandateOptions { get; set; }

        [JsonPropertyName("network")]
        public object Network { get; set; }

        [JsonPropertyName("request_three_d_secure")]
        public string RequestThreeDSecure { get; set; }
    }
    
    public class PaymentMethodConfigurationDetails
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class PaymentMethodOptions
    {
        [JsonPropertyName("card")]
        public Card Card { get; set; }
    }
    
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("allowed_payment_method_types")]
        public object AllowedPaymentMethodTypes { get; set; }

        [JsonPropertyName("amount")]
        public int? Amount { get; set; }

        [JsonPropertyName("amount_capturable")]
        public int? AmountCapturable { get; set; }
        
        [JsonPropertyName("amount_received")]
        public int? AmountReceived { get; set; }

        [JsonPropertyName("application")]
        public object Application { get; set; }

        [JsonPropertyName("application_fee_amount")]
        public object ApplicationFeeAmount { get; set; }
        
        [JsonPropertyName("canceled_at")]
        public object CanceledAt { get; set; }

        [JsonPropertyName("cancellation_reason")]
        public object CancellationReason { get; set; }

        [JsonPropertyName("capture_method")]
        public string CaptureMethod { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }

        [JsonPropertyName("confirmation_method")]
        public string ConfirmationMethod { get; set; }

        [JsonPropertyName("created")]
        public int? Created { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("customer")]
        public object Customer { get; set; }

        [JsonPropertyName("customer_account")]
        public object CustomerAccount { get; set; }

        [JsonPropertyName("description")]
        public object Description { get; set; }

        [JsonPropertyName("excluded_payment_method_types")]
        public object ExcludedPaymentMethodTypes { get; set; }

        [JsonPropertyName("last_payment_error")]
        public object LastPaymentError { get; set; }

        [JsonPropertyName("latest_charge")]
        public object LatestCharge { get; set; }

        [JsonPropertyName("livemode")]
        public bool? Livemode { get; set; }
        
        [JsonPropertyName("next_action")]
        public object NextAction { get; set; }

        [JsonPropertyName("on_behalf_of")]
        public object OnBehalfOf { get; set; }

        [JsonPropertyName("payment_method")]
        public object PaymentMethod { get; set; }
        
        [JsonPropertyName("payment_method_types")]
        public List<string> PaymentMethodTypes { get; set; }

        [JsonPropertyName("payment_record")]
        public object PaymentRecord { get; set; }

        [JsonPropertyName("processing")]
        public object Processing { get; set; }

        [JsonPropertyName("receipt_email")]
        public object ReceiptEmail { get; set; }

        [JsonPropertyName("review")]
        public object Review { get; set; }

        [JsonPropertyName("setup_future_usage")]
        public object SetupFutureUsage { get; set; }

        [JsonPropertyName("shared_payment_granted_token")]
        public object SharedPaymentGrantedToken { get; set; }

        [JsonPropertyName("shipping")]
        public object Shipping { get; set; }

        [JsonPropertyName("source")]
        public object Source { get; set; }

        [JsonPropertyName("statement_descriptor")]
        public object StatementDescriptor { get; set; }

        [JsonPropertyName("statement_descriptor_suffix")]
        public object StatementDescriptorSuffix { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("transfer_data")]
        public object TransferData { get; set; }

        [JsonPropertyName("transfer_group")]
        public object TransferGroup { get; set; }
}