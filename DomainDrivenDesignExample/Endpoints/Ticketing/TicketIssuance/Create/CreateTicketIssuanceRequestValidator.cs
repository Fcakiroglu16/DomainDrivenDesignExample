#region

using FluentValidation;

#endregion

namespace DomainDrivenDesignExample.API.Endpoints.Ticketing.TicketIssuance.Create;

public class CreateTicketIssuanceRequestValidator : AbstractValidator<CreateTicketIssuanceRequest>
{
    public CreateTicketIssuanceRequestValidator()
    {
        RuleFor(x => x.ScheduledMovieShowId).NotEmpty();
    }
}