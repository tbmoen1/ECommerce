using OrderService.Application.Interfaces;
using OrderService.Infrastructure.HttpClients;

namespace OrderService.Infrastructure.Repositories;

public class InventoryRepository(IHttpClientFactory httpClientFactory) : IInventoryRepository
{
    public async Task<string> ReserveUnitAsync(long unitId, int quantity)
    {
        var client = httpClientFactory.CreateClient(nameof(InventoryServiceHttpClient));
        var response = await client.GetAsync($"Units/{unitId}/Reserve/{quantity}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}