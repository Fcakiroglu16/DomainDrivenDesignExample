namespace DomainDrivenDesignExample.API.SharedKernels.Repositories;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}