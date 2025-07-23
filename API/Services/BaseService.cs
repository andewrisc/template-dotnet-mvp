using System;
using API.Data;
using API.Interfaces;

namespace API.Services;

public class BaseService<T> where T : class
{
    protected readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository)
    {
        _repository = repository;
    }

    public void BeginTransaction() => _repository.BeginTransaction();
    public void CommitTransaction() => _repository.CommitTransaction();
    public void RollbackTransaction() => _repository.RollbackTransaction();
}
