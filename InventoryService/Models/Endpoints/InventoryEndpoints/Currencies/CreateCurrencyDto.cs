namespace InventoryService.Models.Endpoints.InventoryEndpoints.Currencies;

public class CreateCurrencyDto(string code, string displayName)
{
    public string Code { get; set; } = code;
    public string DisplayName { get; set; } = displayName;
}