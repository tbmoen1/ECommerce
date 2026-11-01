namespace InventoryService.Domain.Entities;

public class Currency(long id, string displayName, string code)
{
    public long Id { get; set; } = id; 
    public string DisplayName { get; set; } = displayName;
    public string Code { get; set; } = code;
}