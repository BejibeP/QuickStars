using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using QuickStars.MVApi.Domain.Data.Entities;

namespace QuickStars.MVApi.Domain.Data.Interceptors
{
    public class SaveInterceptor : SaveChangesInterceptor
    {

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ApplyTimestampToEntries(eventData, result);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            ApplyTimestampToEntries(eventData, result);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ApplyTimestampToEntries(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData?.Context is null)
                return;

            var changeTracker = eventData.Context.ChangeTracker;

            var entries = changeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is not BaseEntity)
                    continue;

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    ((BaseEntity)entry.Entity).ModifiedOn = DateTime.UtcNow;
                    ((BaseEntity)entry.Entity).ModifiedBy = "Application";
                }
            }
        }

    }
}