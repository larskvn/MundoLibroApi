using System.Linq.Expressions;

namespace Library.Domain.Cores;

public interface ICrudRepository<TEntity, TId>
{
    Task<IReadOnlyList<TEntity>> FindAllAsync();
    
    Task<TEntity?> FindByIdAsync(TId id);
    
    Task<TEntity> SaveAsync(TEntity entity);
    
    Task<IList<TEntity>> SaveAllAsync(List<TEntity> entities);
    
    Task<TEntity?> FindFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        List<Expression<Func<TEntity, object>>> includes = null,
        bool disableTracking = true
    );
    
    Task<IList<TEntity>> FindAllAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        List<Expression<Func<TEntity, object>>> includes = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        bool disableTracking = true);

}