using InventoryService.Application.Interfaces;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Exceptions;
using InventoryService.Infrastructure.Contexts;
using InventoryService.Models.Endpoints.InventoryEndpoints;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Service;

public class InventoryService(InventoryContext context) : IInventoryService
{
    public async Task<Unit> UpdateUnit(UpdateInventoryUnitDto unitDto, Unit unitToUpdate)
    {
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
                throw new Exception($"Currency code {unitDto.Value.CurrencyCode} not found");
            
            unitToUpdate.SetValue(currencyToUpdate.Code, unitDto.Value.Value);
        }

        return unitToUpdate;
    }

    public async Task<Unit> TryReserveAmountOfUnitsAsync(Unit unitToReserve, int quantity)
    {
        try
        {
            unitToReserve.TryReserve(quantity);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

            throw new UnitConcurrencyException("Another request as changed the same row. Please try again");
        }        
        return unitToReserve;
    }
}