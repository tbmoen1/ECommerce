using System.ComponentModel.DataAnnotations;
using InventoryService.Domain.Contracts;
using InventoryService.Domain.Exceptions;

namespace InventoryService.Domain.Entities;

public class Unit : IConcurrency
{
    public long Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<UnitValue>? UnitValues { get; set; }
    // IsVisible is set to false after creation to hide units with no values
    public bool IsVisible { get; set; }
    public int AvailableQuantity { get; set;}
    
    #region Concurrency
    [ConcurrencyCheck]
    public Guid ConcurrencyGuid { get; set; } = Guid.NewGuid();
    #endregion

    public Unit(long id, string name, string description, int? quantity)
    {
        Id = id;
        Name = name;
        Description = description;
        IsVisible = false;
        AvailableQuantity = quantity ?? 0;
    }

    #region EfCore Constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Unit() {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    #endregion
    
    // Set a specific currency value, and sets unit to visible
    public void SetValue(string currencyCode, decimal newValue)
    {
        var unitValue = UnitValues?.Find(value => value.Currency.Code == currencyCode);
        if (unitValue == null)
            throw new CurrencyNotFoundException();
        unitValue.Value = newValue;
        IsVisible = true;
    }

    public void TryReserve(int quantityToReserve)
    {
        var unitsLeft = AvailableQuantity - quantityToReserve;
        if (unitsLeft < 0)
        {
            throw new NoUnitsLeftException("The amount of units reserved is more than unit quantity.");
        }
        AvailableQuantity = unitsLeft;
    }
}