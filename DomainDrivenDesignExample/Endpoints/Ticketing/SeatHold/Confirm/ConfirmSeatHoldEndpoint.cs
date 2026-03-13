#region

using CinemaTicketingSystem.Presentation.API.Extensions;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DomainDrivenDesignExample.API.Endpoints.Ticketing.SeatHold.Confirm;

public static class ConfirmSeatHoldEndpoint
{
    public static RouteGroupBuilder ConfirmSeatHoldGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/seatholds/confirm",
                async (ConfirmSeatHoldRequest request, [FromServices] ISeatHoldApplicationService seatHoldAppService) =>
                (await seatHoldAppService.ConfirmAsync(request)).ToGenericResult())
            .WithName("ConfirmSeatHold");
        //.MapToApiVersion(1, 0)
        //.AddEndpointFilter<ValidationFilter<ConfirmSeatHoldRequest>>();


        return group;
    }
}