using InventoryService.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InventoryService.Infrastructure.Data.Interceptors;

public sealed class ConcurrencyInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateConcurrencyTokens(eventData.Context);
        return result;
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateConcurrencyTokens(eventData.Context);
        return ValueTask.FromResult(result);
    }
    
    private static void UpdateConcurrencyTokens(DbContext? context)
    {
        if (context is null)
            return;

        foreach (var entry in context.ChangeTracker.Entries<IConcurrency>())
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Property(nameof(IConcurrency.ConcurrencyGuid))
                    .CurrentValue = Guid.NewGuid();
            }
        }
    }
}