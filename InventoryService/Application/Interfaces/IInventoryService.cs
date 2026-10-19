using InventoryService.Domain.Entities;
using InventoryService.Models.Endpoints.InventoryEndpoints;

namespace InventoryService.Application.Interfaces;

public interface IInventoryService
{
    public Task<Unit> UpdateUnit(UpdateInventoryUnitDto unitDto, Unit unitToUpdate);
}