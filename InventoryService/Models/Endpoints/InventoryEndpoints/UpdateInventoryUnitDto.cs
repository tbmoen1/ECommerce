using InventoryService.Domain.Entities;

namespace InventoryService.Models.Endpoints.InventoryEndpoints;

public class UpdateInventoryUnitDto(string name, string description)
{
    public string? Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public UpdateUnitValueDto? Value { get; set; }
}

public class UpdateUnitValueDto
{
    public required string CurrencyCode { get; set; }
    public required decimal Value { get; set; }
}

public class CreateUnitDto(string name, string description, int? quantity, UpdateUnitValueDto? unitValue)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public int? Quantity { get; set; } = quantity;
    public UpdateUnitValueDto? UnitValue { get; set; } = unitValue;
}