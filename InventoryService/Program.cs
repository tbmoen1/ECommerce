using InventoryService.Api.Endpoints.InventoryEndpoints;
using InventoryService.Application.Interfaces;
using InventoryService.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEntityFrameworkConfigurations();
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

app.Run();