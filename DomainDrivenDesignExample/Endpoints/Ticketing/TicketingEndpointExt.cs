#region

using DomainDrivenDesignExample.API.Endpoints.Ticketing.SeatHold.Confirm;
using DomainDrivenDesignExample.API.Endpoints.Ticketing.SeatHold.Create;
using DomainDrivenDesignExample.API.Endpoints.Ticketing.TicketIssuance.Create;

#endregion

namespace DomainDrivenDesignExample.API.Endpoints.Ticketing;

public static class TicketingEndpointExt
{
    public static void AddTicketingGroupEndpointExt(this WebApplication app)
    {
        //api/v1/ticketing/create
        app.MapGroup("api/v{version:apiVersion}/ticketing").WithTags("ticketing")
            .ConfirmSeatHoldGroupItemEndpoint()
            .CreateSeatHoldGroupItemEndpoint()
            .PurchaseTicketsGroupItemEndpoint();
    }
}