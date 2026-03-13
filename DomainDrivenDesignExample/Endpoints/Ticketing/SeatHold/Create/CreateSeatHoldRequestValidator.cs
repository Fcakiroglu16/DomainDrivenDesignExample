#region

using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using FluentValidation;

#endregion

namespace DomainDrivenDesignExample.API.Endpoints.Ticketing.SeatHold.Create;

public class CreateSeatHoldRequestValidator : AbstractValidator<CreateSeatHoldRequest>
{
    public CreateSeatHoldRequestValidator()
    {
        RuleFor(x => x.SeatPositions).NotEmpty();
        RuleFor(x => x.ScheduledMovieShowId).NotEmpty();
    }
}