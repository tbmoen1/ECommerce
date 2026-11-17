using Microsoft.AspNetCore.Mvc;
using PaymentService.Application;
using PaymentService.Infrastructure.Payments.Stripe.Models;

namespace PaymentService.Api.Endpoints.CheckoutSession;

public static class CreateCheckoutSession
{
    public static void MapCreateCheckoutSessionEndpoints(this IApplicationBuilder app, RouteGroupBuilder group)
    {
        group.MapPost("CheckoutSession/Create", CreateCheckoutSessionAsync)
            .WithDescription("NB: A 200 OK from this endpoint, does not mean a successful payment");
    }

    public async static Task<IResult> CreateCheckoutSessionAsync(
        [FromServices] IPaymentProvider paymentProvider,
        [FromBody] CreateCheckoutSessionDto dto)
    {
        var intent = await paymentProvider.CreateCheckoutSessionAsync(dto);
        return TypedResults.Ok(intent);
    }
}