using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Contexts;

namespace OrderService.Infrastructure.Configurations;

public static class EntityFrameworkConfigurations
{
    public static void AddEntityFrameworkConfigurations(this IServiceCollection services)
    {
        services.AddPooledDbContextFactory<DomainContext>(options =>
        {
            options.UseSqlite("Data Source=orders.db");
        }); 
    }

    public static void UseEntityFrameworkConfigurations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<DomainContext>();
        context.Database.EnsureCreated();
    }
}