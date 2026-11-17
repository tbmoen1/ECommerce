using Microsoft.AspNetCore.Mvc;
using PaymentService.Application;
using PaymentService.Infrastructure.Prices.Stripe.Models;

namespace PaymentService.Api.Endpoints.Price;

public static class CreatePrice
{
    public static void MapCreatePriceEndpoints(this IApplicationBuilder app, RouteGroupBuilder group)
    {
        group.MapPost("Price/Create", CreatePriceAsync);
    }

    public static async Task<IResult> CreatePriceAsync(
        [FromServices] IPriceProvider priceProvider,
        [FromBody] CreatePriceDto dto)
    {
        var price = await priceProvider.CreatePrice(dto);
        return TypedResults.Ok(price);
    }
}
