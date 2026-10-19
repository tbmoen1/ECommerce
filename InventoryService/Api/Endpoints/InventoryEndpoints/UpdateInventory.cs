using InventoryService.Application.Interfaces;
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
            [FromBody] UpdateInventoryUnitDto unitDto,
            [FromServices] IInventoryService inventoryService)
    {
        var unitToUpdate = await context.Units.FirstOrDefaultAsync(u => u.Id == unitId);
        if (unitToUpdate == null)
            return new NotFoundResult();
        
        await inventoryService.UpdateUnit(unitDto, unitToUpdate);
        await context.SaveChangesAsync();
        return unitToUpdate;
    }
}