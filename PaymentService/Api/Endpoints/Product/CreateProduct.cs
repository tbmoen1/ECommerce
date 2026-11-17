using Microsoft.AspNetCore.Mvc;
using PaymentService.Application;
using PaymentService.Infrastructure.Products.Stripe.Models;

namespace PaymentService.Api.Endpoints.Product;

public static class CreateProduct
{
    public static void MapCreateProductEndpoints(this IApplicationBuilder app, RouteGroupBuilder group)
    {
        group.MapPost("Product/Create", CreateProductAsync);
    }

    public static async Task<IResult> CreateProductAsync(
        [FromServices] IProductProvider productProvider,
        [FromBody] CreateProductDto dto)
    {
        var product = await productProvider.CreateProduct(dto);
        return TypedResults.Ok(product);
    }
}
