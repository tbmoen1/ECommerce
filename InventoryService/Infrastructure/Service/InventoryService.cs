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

        if (unitDto.Description != null)
        {
            unitToUpdate.Description = unitDto.Description;
        }

        if (unitDto.Value != null)
        {
            await UpdateUnitCurrency(unitDto.Value, unitToUpdate);
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

    public async Task UpdateUnitCurrency(UpdateUnitValueDto updateUnitValueDto, Unit unitToUpdate)
    {
        var currencyToUpdate =
            await context 
                .Currencies.FirstOrDefaultAsync(c => c.Code == updateUnitValueDto.CurrencyCode);
        
        if (currencyToUpdate == null)
            throw new Exception($"Currency code {updateUnitValueDto.CurrencyCode} not found");
        
        unitToUpdate.SetValue(currencyToUpdate.Code, updateUnitValueDto.Value);
    }
}