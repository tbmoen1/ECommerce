namespace OrderService.Application.Interfaces;

public interface IInventoryRepository
{
    public Task<string> ReserveUnitAsync(long unitId, int quantity);
}