#region

using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using DomainDrivenDesignExample.API.SharedKernels.Repositories;

#endregion

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> CommitAsync()
    {
        return context.SaveChangesAsync();
    }
}