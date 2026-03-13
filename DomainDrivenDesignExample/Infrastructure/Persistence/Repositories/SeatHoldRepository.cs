#region

using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;

public class SeatHoldRepository(AppDbContext context) : GenericRepository<SeatHold>(context), ISeatHoldRepository
{
    public Task<List<SeatHold>> GetConfirmedListByScheduleIdAndScreeningDate(Guid scheduledMovieShowId,
        DateOnly screeningDate)
    {
        return Context.SeatHolds
            .Where(x => x.ScheduledMovieShowId == scheduledMovieShowId && x.ScreeningDate == screeningDate &&
                        x.Status == HoldStatus.Hold && x.ExpiresAt > DateTime.UtcNow)
            .ToListAsync();
    }
}