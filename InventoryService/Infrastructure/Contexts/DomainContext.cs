using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Contexts;

public class InventoryContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Unit> Units { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<UnitValue> UnitValues { get; set; }
}