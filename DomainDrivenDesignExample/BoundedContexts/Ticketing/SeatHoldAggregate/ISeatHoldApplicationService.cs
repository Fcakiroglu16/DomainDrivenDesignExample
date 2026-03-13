using DomainDrivenDesignExample.API.SharedKernels;

namespace DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;

public record ConfirmSeatHoldRequest(Guid ScheduledMovieShowId, DateOnly ScreeningDate);

public record CreateSeatHoldRequest(
    List<SeatPositionDto> SeatPositions,
    Guid ScheduledMovieShowId,
    DateOnly ScreeningDate);

public record SeatPositionDto(string Row, int Number)
{
}

public interface ISeatHoldApplicationService
{
    Task<AppResult> CreateAsync(CreateSeatHoldRequest request);
    Task<AppResult> ConfirmAsync(ConfirmSeatHoldRequest request);
}