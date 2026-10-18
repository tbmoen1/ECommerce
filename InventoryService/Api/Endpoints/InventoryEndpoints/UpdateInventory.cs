using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Contexts;
using InventoryService.Models.Endpoints.InventoryEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Api.Endpoints.InventoryEndpoints;

public static class UpdateInventoryEndpoints
{
    public static void MapUpdateInventoryEndpoints(this WebApplication app, RouteGroupBuilder group)
    {
        group.MapPost("Units/{unitId:long}/Update", UpdateUnit);
    }

    [EndpointDescription("Updates the Unit. New data will override existing, if sent")]
    public static async Task<ActionResult<Unit>> UpdateUnit(
        InventoryContext context,
        long unitId,
        [FromBody] UpdateInventoryUnitDto unitDto)
    {
        var unitToUpdate = await context.Units.FirstOrDefaultAsync(u => u.Id == unitId);
        if (unitToUpdate == null)
            return new NotFoundResult();
        
        // Extract to its own service, UnitService fx
        if (unitDto.Name != null)
        {
            unitToUpdate.Name = unitDto.Name;
        } 
        else if (unitDto.Description != null)
        {
            unitToUpdate.Description = unitDto.Description;
        }
        else if (unitDto.Value != null)
        {
            var currencyToUpdate =
                await context 
                    .Currencies.FirstOrDefaultAsync(c => c.Code == unitDto.Value.CurrencyCode);
            if (currencyToUpdate == null)
                return new NotFoundResult();
            
            unitToUpdate.SetValue(currencyToUpdate.Code, unitDto.Value.Value);
        }

        await context.SaveChangesAsync();
        return unitToUpdate;
    }
}