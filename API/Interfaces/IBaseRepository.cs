using System;
using System.Linq.Expressions;

namespace API.Interfaces;

public interface IBaseRepository<T>
{
    Task<IReadOnlyList<T>> GetReadonlyByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges); //use this if u know the data is small
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    Task AddAsync(T entity);
    Task AddRangeAsync(List<T> entity);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(List<T> entity);
    Task SaveChangesAsync();
    void BeginTransaction();
    void RollbackTransaction();
    void CommitTransaction();
}
