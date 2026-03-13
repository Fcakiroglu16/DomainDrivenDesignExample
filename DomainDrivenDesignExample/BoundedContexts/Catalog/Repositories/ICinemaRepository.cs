#region

using DomainDrivenDesignExample.API.SharedKernels.Repositories;

#endregion

namespace DomainDrivenDesignExample.API.BoundedContexts.Catalog.Repositories;

public interface ICinemaRepository : IGenericRepository<Cinema>
{
    Task<Cinema?> GetByHallId(Guid hallId);
}