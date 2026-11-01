using ECommerce.IdGeneration;
using InventoryService.Api.Endpoints.InventoryEndpoints;
using InventoryService.Api.Endpoints.InventoryEndpoints.Currencies;
using InventoryService.Application.Interfaces;
using InventoryService.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEntityFrameworkConfigurations();
builder.Services.AddSnowflakeIdGenerator(
    builder.Configuration.GetValue<long>("IdGenerator:NodeId"));
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IInventoryService, InventoryService.Infrastructure.Service.InventoryService>(); // Probably shouldnt have named project "InventoryService"...

var app = builder.Build();

app.UseEntityFrameworkConfigurations();
app.UseSwagger();
app.UseSwaggerUI();

var inventoryGroup = app.MapGroup("inventory");

app.MapReadInventoryEndpoints(inventoryGroup);
app.MapUpdateInventoryEndpoints(inventoryGroup);
app.MapCreateCurrencyEndpoints(inventoryGroup);

app.Run();