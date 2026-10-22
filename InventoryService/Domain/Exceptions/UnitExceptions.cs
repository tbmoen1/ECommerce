namespace InventoryService.Domain.Exceptions;

public class NoUnitsLeftException(string message) : Exception(message) {}
public class UnitConcurrencyException(string message) : Exception(message) {}