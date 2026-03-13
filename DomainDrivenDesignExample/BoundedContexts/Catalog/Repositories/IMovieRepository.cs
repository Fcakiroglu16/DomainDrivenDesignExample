#region

using DomainDrivenDesignExample.API.SharedKernels.Repositories;

#endregion

namespace DomainDrivenDesignExample.API.BoundedContexts.Catalog.Repositories;

public interface IMovieRepository : IGenericRepository<Movie>
{
    Task<bool> CheckIfMovieExists(string title);
}