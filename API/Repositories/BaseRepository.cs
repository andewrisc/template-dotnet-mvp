using System;
using System.Linq.Expressions;
using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected AppDbContext _context;
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
    public IQueryable<T> FindAll(bool trackChanges) =>
     trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        FindAll(trackChanges).Where(expression);

    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
    public async Task AddRangeAsync(List<T> entity) => await _context.Set<T>().AddRangeAsync(entity);

    public void Update(T entity)
    {
        _context.Set<T>().Entry(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    public void Remove(T entity) => _context.Set<T>().Remove(entity);
    public void RemoveRange(List<T> entity) => _context.Set<T>().RemoveRange(entity);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    public void BeginTransaction() => _context.Database.BeginTransaction();
    public void RollbackTransaction() => _context.Database.RollbackTransaction();
    public void CommitTransaction() => _context.Database.CommitTransaction();

    public async Task<IReadOnlyList<T>> GetReadonlyByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) => await FindByCondition(expression, trackChanges).ToListAsync();
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression) => await _context.Set<T>().AnyAsync(expression);
}
