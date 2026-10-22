using InventoryService.Infrastructure.Contexts;
using InventoryService.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Configurations;

public static class EntityFrameworkConfigurations
{
    public static void AddEntityFrameworkConfigurations(this IServiceCollection services)
    {
        services.AddPooledDbContextFactory<InventoryContext>(options =>
        {
            options.UseSqlite("Data Source=inventory.db");
            options.AddInterceptors(new ConcurrencyInterceptor());
            options.EnableSensitiveDataLogging();
        }); 
    }

    public static void UseEntityFrameworkConfigurations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<InventoryContext>();
        context.Database.EnsureCreated();
    }
}