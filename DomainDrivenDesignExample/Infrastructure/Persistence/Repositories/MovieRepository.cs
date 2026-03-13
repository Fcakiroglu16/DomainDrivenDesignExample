#region

using DomainDrivenDesignExample.API.BoundedContexts.Catalog;
using DomainDrivenDesignExample.API.BoundedContexts.Catalog.Repositories;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;

internal class MovieRepository(AppDbContext context) : GenericRepository<Movie>(context), IMovieRepository
{
    public Task<bool> CheckIfMovieExists(string title)
    {
        return Context.Movies.AnyAsync(m =>
            m.Title.Equals(title));
    }
}