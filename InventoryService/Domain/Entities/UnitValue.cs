namespace InventoryService.Domain.Entities;

public class UnitValue
{
    public required long Id { get; set; }
    public required long UnitId { get; set; }
    public required Currency Currency { get; set; }
    public required decimal Value { get; set; } 
}