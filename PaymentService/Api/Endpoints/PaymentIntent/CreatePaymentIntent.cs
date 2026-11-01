using Microsoft.AspNetCore.Mvc;
using PaymentService.Application;
using PaymentService.Infrastructure.Payments.Stripe.Models;

namespace PaymentService.Api.Endpoints.PaymentIntent;

public static class CreatePaymentIntent
{
    public static void MapCreatePaymentIntentEndpoints(this IApplicationBuilder app, RouteGroupBuilder group)
    {
        group.MapPost("PaymentIntent/CreatePaymentIntent", CreatePaymentIntentAsync)
            .WithDescription("NB: A 200 OK from this endpoint, does not mean a successful payment");
    }

    public async static Task<IResult> CreatePaymentIntentAsync(
        [FromServices] IPaymentProvider paymentProvider,
        [FromBody] CreatePaymentIntentDto dto)
    {
        var intent = await paymentProvider.CreatePaymentIntentAsync(dto);
        return TypedResults.Ok(intent);
    }
}