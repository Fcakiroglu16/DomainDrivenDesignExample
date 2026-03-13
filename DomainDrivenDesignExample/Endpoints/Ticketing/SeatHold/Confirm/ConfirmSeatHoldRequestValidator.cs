#region

using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using FluentValidation;

#endregion

namespace DomainDrivenDesignExample.API.Endpoints.Ticketing.SeatHold.Confirm;

public class ConfirmSeatHoldRequestValidator : AbstractValidator<ConfirmSeatHoldRequest>
{
    public ConfirmSeatHoldRequestValidator()
    {
        RuleFor(x => x.ScheduledMovieShowId).NotEmpty();
    }
}