using System.ComponentModel.DataAnnotations;

namespace InventoryService.Domain.Contracts;

public interface IConcurrency
{
    [ConcurrencyCheck]
    public Guid ConcurrencyGuid { get; set; }
}