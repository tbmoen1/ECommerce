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