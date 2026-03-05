using DomainDrivenDesignExample.API.Endpoints.Ticketing.TicketIssuance.Create;
using DomainDrivenDesignExample.API.SharedKernels;

namespace DomainDrivenDesignExample.API.BoundedContexts.Ticketing.TicketingAggregate;

public interface ITicketIssuanceApplicationService
{
    Task<AppResult<CreateTicketIssuanceResponse>> Create(CreateTicketIssuanceRequest request);
}