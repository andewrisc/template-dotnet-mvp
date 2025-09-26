using System;
using API.Data;
using API.Interfaces;
using API.Models.Base;


namespace API.Services;

public class BaseService<T> where T : class
{
    protected readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository)
    {
        _repository = repository;
    }

    protected void BeginTransaction() => _repository.BeginTransaction();
    protected void CommitTransaction() => _repository.CommitTransaction();
    protected void RollbackTransaction() => _repository.RollbackTransaction();

    public async Task<TOutput> UsingTransaction<TOutput>(Func<Task<TOutput>> func)
    where TOutput : BaseResponse, new()
    {
        TOutput result = new TOutput();
        try
        {
            this.BeginTransaction(); 

            result = await func();

            if (!result.IsSuccessAndValid())
            {
                this.RollbackTransaction();
                return result;
            }

            this.CommitTransaction();
        }
        catch (Exception ex)
        {
            this.RollbackTransaction();
            result.SetErrorMessage(ex.Message);
        }

        return result;
    }

}
