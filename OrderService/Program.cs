using Microsoft.EntityFrameworkCore;
using OrderService.Api.Configurations;
using OrderService.Infrastructure.Configurations;
using OrderService.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConfiguredGraphQl();
builder.Services.AddEntityFrameworkConfigurations();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGraphQL();

// Extract into own UseEntityFramework.cs
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DomainContext>();
    context.Database.EnsureCreated();
}
app.Run();