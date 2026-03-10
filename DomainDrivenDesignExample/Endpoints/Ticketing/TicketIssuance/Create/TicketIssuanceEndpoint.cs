#region

using CinemaTicketingSystem.Presentation.API.Extensions;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.TicketingAggregate;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DomainDrivenDesignExample.API.Endpoints.Ticketing.TicketIssuance.Create;

public static class TicketIssuanceEndpoint
{
    public static RouteGroupBuilder PurchaseTicketsGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/issuance",
            async (CreateTicketIssuanceRequest request,
                    [FromServices] ITicketIssuanceApplicationService purchaseAppService) =>
                (await purchaseAppService.Create(request)).ToGenericResult()).WithName("TicketIssuance");

        //.MapToApiVersion(1, 0)
        //.AddEndpointFilter<ValidationFilter<CreateReservationRequest>>()


        return group;
    }
}