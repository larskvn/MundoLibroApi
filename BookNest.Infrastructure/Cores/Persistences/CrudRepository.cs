using System.Linq.Expressions;
using BookNest.Infrastructure.Cores.Contexts;
using Library.Domain.Cores;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure.Cores.Persistences;

public class CrudRepository<TEntity,TId>: ICrudRepository<TEntity,TId>
where TEntity:class
{
    protected InfrastructureDbContext Context { get; }

    public CrudRepository(InfrastructureDbContext context)
    {
        Context = context;
    }
    
    public async Task<IReadOnlyList<TEntity>> FindAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> FindByIdAsync(TId id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        EntityState? entityState = Context.Entry(entity).State;
        _ = entityState switch
        {
            EntityState.Detached => Context.Set<TEntity>().Add(entity),
            EntityState.Modified => Context.Set<TEntity>().Update(entity),
            _ => throw new ArgumentOutOfRangeException()
        };
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<IList<TEntity>> SaveAllAsync(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            EntityState? entityState = Context.Entry(entity).State;
            _ = entityState switch
            {
                EntityState.Detached => Context.Set<TEntity>().Add(entity),
                EntityState.Modified => Context.Set<TEntity>().Update(entity),
                _ => throw new ArgumentOutOfRangeException()

            };
        }
        await Context.SaveChangesAsync(); 
        return entities;
    }

    public Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes = null, bool disableTracking = true)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        if(disableTracking)
            query = query.AsNoTracking();
        
        if (includes != null)
            query = includes.Aggregate(query, (current, include) 
                => current.Include(include));
        return query.FirstOrDefaultAsync(predicate);
    }

    public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        if (disableTracking)
            query = query.AsNoTracking();
        if (includes != null)
            query = includes.Aggregate(query, (current, include)
                => current.Include(include));
        if (predicate != null)
            query = query.Where(predicate);
        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        
        return await query.ToListAsync();
    }
}