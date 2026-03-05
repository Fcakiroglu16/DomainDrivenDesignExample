namespace DomainDrivenDesignExample.API.Endpoints.Ticketing.TicketIssuance.Create;

public record CreateTicketIssuanceRequest(Guid ScheduledMovieShowId, DateOnly ScreeningDate);