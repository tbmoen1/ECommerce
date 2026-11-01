using PaymentService.Api.Endpoints.PaymentIntent;
using PaymentService.Application;
using PaymentService.Infrastructure.HttpClients.Payments;
using PaymentService.Infrastructure.Payments.Stripe;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStripeHttpClient();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddTransient<IPaymentProvider, StripePaymentProvider>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var group = app.MapGroup("payments");
app.MapCreatePaymentIntentEndpoints(group);
app.Run();