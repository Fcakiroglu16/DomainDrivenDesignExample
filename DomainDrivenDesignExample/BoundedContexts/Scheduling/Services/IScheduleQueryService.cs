using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.ValueObjects;

namespace DomainDrivenDesignExample.API.BoundedContexts.Scheduling.Services;

public record GetScheduleInfoResponse(Guid CinemaHallId, Guid MovieId, ShowTime ShowTime, Price TicketPrice);

public interface IScheduleQueryService
{
    Task<AppResult<GetScheduleInfoResponse>> GetScheduleInfo(Guid scheduledMovieShowId);
}