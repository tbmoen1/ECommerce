using HotChocolate.AspNetCore;

namespace OrderService.Api.Configurations;

public static class GraphQlConfiguration
{
    public static void AddConfiguredGraphQl(this IServiceCollection services)
    {
        services.AddGraphQLServer(nameof(OrderService))
            .AddInMemorySubscriptions()
            .AddQueryType()
            .AddProjections()   
            .AddFiltering()
            .AddSorting()
            .AddTypes()
            .AddFiltering(descriptor =>
            {
                descriptor.AddDefaults();
            })
            .ModifyPagingOptions(options =>
            {
                options.DefaultPageSize = 100;
                options.IncludeTotalCount = true;
            })
            .ModifyServerOptions(options =>
            {
                options.Batching = AllowedBatching.All;
            })
            .ModifyOptions(options =>
            {
                options.DefaultQueryDependencyInjectionScope = DependencyInjectionScope.Request;
            })
            .ModifyRequestOptions(options =>
            {
                options.IncludeExceptionDetails = true;
            });
    }
}