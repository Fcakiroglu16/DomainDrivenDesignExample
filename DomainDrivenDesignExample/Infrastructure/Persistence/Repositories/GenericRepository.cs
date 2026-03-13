#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.Repositories;

#endregion

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
{
    protected readonly AppDbContext Context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<TEntity> result = await _dbSet.AddAsync(entity, cancellationToken);
        return result.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(object[] keys, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(keys, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(object key, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([key], cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }


    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.UpdateRange(entities);
        return Task.CompletedTask;
    }


    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        List<TEntity> entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        _dbSet.RemoveRange(entities);
    }

    public async Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool ascending = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        // Apply filter if provided
        if (predicate != null)
            query = query.Where(predicate);

        // Get total count before pagination
        int totalCount = await query.CountAsync(cancellationToken);

        // Apply ordering
        if (orderBy != null) query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

        // Apply pagination
        List<TEntity> items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}