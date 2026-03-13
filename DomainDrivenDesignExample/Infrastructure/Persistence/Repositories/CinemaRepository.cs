#region

using DomainDrivenDesignExample.API.BoundedContexts.Catalog;
using DomainDrivenDesignExample.API.BoundedContexts.Catalog.Repositories;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;

public class CinemaRepository(AppDbContext context) : GenericRepository<Cinema>(context), ICinemaRepository

{
    public Task<Cinema?> GetByHallId(Guid hallId)
    {
        return Context.Cinemas
            .Include(c => c.Halls)
            .FirstOrDefaultAsync(c => c.Halls.Any(h => h.Id == hallId));
    }
}