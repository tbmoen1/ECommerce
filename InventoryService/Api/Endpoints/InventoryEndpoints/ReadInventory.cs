using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Api.Endpoints.InventoryEndpoints;

public static class ReadInventory
{
    public static void MapReadInventoryEndpoints(this WebApplication app, RouteGroupBuilder group)
    {
        group.MapGet("Units", ListUnits);
        group.MapGet("Units/{unitId:long}", GetUnitById);
    }

    public static async Task<List<Unit>> ListUnits(InventoryContext context)
    {
        return await context.Units.ToListAsync();
    }
    
    public static async Task<ActionResult<Unit>> GetUnitById(InventoryContext context, long unitId)
    {
        var unit =  await context.Units.FirstOrDefaultAsync(u=> u.Id == unitId);
        if (unit == null)
            return new NotFoundResult();
        
        return unit;
    }
}