namespace OrderService.Infrastructure.HttpClients;

public static class InventoryServiceHttpClient
{
    public static void AddInventoryServiceHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(InventoryServiceHttpClient),client =>
        {
            client.BaseAddress = new Uri("https://localhost:5059");
        });
    }
}