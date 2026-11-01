using ECommerce.IdGeneration;
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Contexts;
using InventoryService.Models.Endpoints.InventoryEndpoints.Currencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Api.Endpoints.InventoryEndpoints.Currencies;

public static class CreateCurrency
{
    public static void MapCreateCurrencyEndpoints(this WebApplication app, RouteGroupBuilder group)
    {
        group.MapPost("Currency", CreateCurrencyAsync)
            .Produces<Currency>(StatusCodes.Status201Created);
    }

    public async static Task<IResult> CreateCurrencyAsync(
        [FromServices] InventoryContext context,
        [FromServices] IIdGenerator idGenerator,
        [FromBody] CreateCurrencyDto createCurrencyDto)
    {
       var currencyInDb = await context.Currencies.FirstOrDefaultAsync(c => c.Code == createCurrencyDto.Code);
       if (currencyInDb != null)
       {
           return TypedResults.BadRequest("Currency already exists");
       }

       var currencyToAdd = new Currency(
           idGenerator.NextId(), 
           createCurrencyDto.DisplayName,
           createCurrencyDto.Code);
       
       await context.Currencies.AddAsync(currencyToAdd);
       await context.SaveChangesAsync();
       return TypedResults.Created($"api/currencies/{currencyToAdd.Id}", currencyToAdd);
    }
}