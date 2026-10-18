using Microsoft.EntityFrameworkCore;
using OrderService.Api.Configurations;
using OrderService.Infrastructure.Configurations;
using OrderService.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConfiguredGraphQl();
builder.Services.AddEntityFrameworkConfigurations();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGraphQL("/graphql/orders");
app.UseEntityFrameworkConfigurations();

app.Run();