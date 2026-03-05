using DomainDrivenDesignExample.API.SharedKernels.Repositories;

namespace DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;

public interface ISeatHoldRepository : IGenericRepository<SeatHold>
{
    Task<List<SeatHold>> GetConfirmedListByScheduleIdAndScreeningDate(Guid scheduledMovieShowId,
        DateOnly ScreeningDate);
}