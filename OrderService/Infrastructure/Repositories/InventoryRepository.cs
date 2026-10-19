using OrderService.Infrastructure.HttpClients;

namespace OrderService.Infrastructure.Repositories;

public class InventoryRepository(IHttpClientFactory httpClientFactory)
{
    public async Task<string> GetUnitAsync(int unitId)
    {
        var client = httpClientFactory.CreateClient(nameof(InventoryServiceHttpClient));
        var response = await client.GetAsync($"/api/units/{unitId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}