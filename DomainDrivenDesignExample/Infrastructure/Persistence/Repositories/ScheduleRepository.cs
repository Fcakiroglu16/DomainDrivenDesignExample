#region

using DomainDrivenDesignExample.API.BoundedContexts.Scheduling;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;

internal class ScheduleRepository(AppDbContext context)
    : GenericRepository<Schedule>(context), IScheduleRepository
{
    public Task<List<Schedule>> GetMoviesByHallIdAsync(Guid hallId, CancellationToken cancellationToken = default)
    {
        return Context.Schedules
            .Where(x => x.HallId == hallId).OrderBy(x => x.ShowTime.StartTime)
            .ToListAsync(cancellationToken);
    }
}