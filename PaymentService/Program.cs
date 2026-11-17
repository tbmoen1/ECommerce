using PaymentService.Api.Endpoints.CheckoutSession;
using PaymentService.Api.Endpoints.Price;
using PaymentService.Api.Endpoints.Product;
using PaymentService.Application;
using PaymentService.Infrastructure.HttpClients.Payments;
using PaymentService.Infrastructure.Payments.Stripe;
using PaymentService.Infrastructure.Prices.Stripe;
using PaymentService.Infrastructure.Products.Stripe;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStripeHttpClient();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddTransient<IPaymentProvider, StripePaymentProvider>();
builder.Services.AddTransient<IProductProvider, StripeProductProvider>();
builder.Services.AddTransient<IPriceProvider, StripePriceProvider>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var group = app.MapGroup("payments");
app.MapCreateCheckoutSessionEndpoints(group);
app.MapCreateProductEndpoints(group);
app.MapCreatePriceEndpoints(group);
app.Run();
