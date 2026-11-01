using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.IdGeneration;

public static class ServiceCollectionExtensions
{
    public static void AddSnowflakeIdGenerator(
        this IServiceCollection services, long nodeId)
    {
        services.AddSingleton<IIdGenerator>(new SnowflakeIdGenerator(nodeId));
    }
}
