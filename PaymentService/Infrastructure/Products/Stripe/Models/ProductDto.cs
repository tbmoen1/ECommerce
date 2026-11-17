using System.Text.Json.Serialization;

namespace PaymentService.Infrastructure.Products.Stripe.Models;

public class ProductDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("object")]
    public string Object { get; set; }

    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    [JsonPropertyName("created")]
    public int? Created { get; set; }

    [JsonPropertyName("default_price")]
    public object DefaultPrice { get; set; }

    [JsonPropertyName("description")]
    public object Description { get; set; }

    [JsonPropertyName("images")]
    public List<object> Images { get; set; }

    [JsonPropertyName("marketing_features")]
    public List<object> MarketingFeatures { get; set; }

    [JsonPropertyName("livemode")]
    public bool? Livemode { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, string> Metadata { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("package_dimensions")]
    public object PackageDimensions { get; set; }

    [JsonPropertyName("shippable")]
    public object Shippable { get; set; }

    [JsonPropertyName("statement_descriptor")]
    public object StatementDescriptor { get; set; }

    [JsonPropertyName("tax_code")]
    public object TaxCode { get; set; }

    [JsonPropertyName("unit_label")]
    public object UnitLabel { get; set; }

    [JsonPropertyName("updated")]
    public int? Updated { get; set; }

    [JsonPropertyName("url")]
    public object Url { get; set; }
}
