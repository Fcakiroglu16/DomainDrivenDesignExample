namespace DomainDrivenDesignExample.API.BoundexContexts.Ticketing;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}