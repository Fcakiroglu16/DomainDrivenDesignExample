#region

using CinemaTicketingSystem.Presentation.API.Extensions;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DomainDrivenDesignExample.API.Endpoints.Ticketing.SeatHold.Create;

public static class CreateSeatHoldEndpoint
{
    public static RouteGroupBuilder CreateSeatHoldGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/seatholds",
                async (CreateSeatHoldRequest request, [FromServices] ISeatHoldApplicationService seatHoldAppService) =>
                (await seatHoldAppService.CreateAsync(request)).ToGenericResult())
            .WithName("CreateSeatHold");
        //.MapToApiVersion(1, 0)
        //.AddEndpointFilter<ValidationFilter<CreateSeatHoldRequest>>();


        return group;
    }
}