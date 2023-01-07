using ConfigurationBag.Core.Common.Entities;
using ConfigurationBag.Core.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationBag.Infrastructure.Data.SqlServer;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    public DbContext DbContext { get; }
    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public Repository(DbContext dbContext)
    {
        DbContext = dbContext;

        Entities = DbContext.Set<TEntity>();
    }

    public virtual ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return Entities.FindAsync(ids, cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Entities.Update(entity);

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Entities.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        Entities.RemoveRange(entities);

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task SaveAsync(CancellationToken cancellationToken)
    {
        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}