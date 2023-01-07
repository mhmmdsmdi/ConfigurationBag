using ConfigurationBag.Core.Common.Entities;

namespace ConfigurationBag.Core.Common.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> Table { get; }
    IQueryable<TEntity> TableNoTracking { get; }

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task SaveAsync(CancellationToken cancellationToken);
}