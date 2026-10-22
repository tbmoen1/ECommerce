using InventoryService.Application.Interfaces;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Exceptions;
using InventoryService.Infrastructure.Contexts;
using InventoryService.Models.Endpoints.InventoryEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Api.Endpoints.InventoryEndpoints;

public static class UpdateInventoryEndpoints
{
    public static void MapUpdateInventoryEndpoints(this WebApplication app, RouteGroupBuilder group)
    {
        group.MapPost("Units/{unitId:long}/Update", UpdateUnit)
            .Produces<Unit>();
        group.MapPost("Units/{unitId:long}/Reserve/{quantity:int}", ReserveAmountOfUnits)
            .Produces<Unit>();
    }

    [EndpointDescription("Updates the Unit. New data will override existing, if sent")]
    public static async Task<IResult> UpdateUnit(
            [FromServices] InventoryContext context,
            long unitId,
            [FromBody] UpdateInventoryUnitDto unitDto,
            [FromServices] IInventoryService inventoryService)
    {
        var unitToUpdate = await context.Units.FirstOrDefaultAsync(u => u.Id == unitId);
        if (unitToUpdate == null)
            return Results.NotFound();
        
        await inventoryService.UpdateUnit(unitDto, unitToUpdate);
        await context.SaveChangesAsync();
        return TypedResults.Ok(unitToUpdate);
    }
    
    public static async Task<IResult> ReserveAmountOfUnits(
        [FromServices] InventoryContext context,
        long unitId,
        int quantity,
        [FromServices] IInventoryService inventoryService)
    {
        var unitToReserve = await context.Units.AsTracking()
            .FirstOrDefaultAsync(u => u.Id == unitId);
        if (unitToReserve == null)
            return Results.NotFound();

        try
        {
            await inventoryService.TryReserveAmountOfUnitsAsync(unitToReserve, quantity);
        }
        catch (NoUnitsLeftException e)
        {
            return Results.InternalServerError(e.Message);
        }
        catch (UnitConcurrencyException e)
        {
            return Results.Conflict(e.Message);
        }
        return TypedResults.Ok(unitToReserve);
    }
}