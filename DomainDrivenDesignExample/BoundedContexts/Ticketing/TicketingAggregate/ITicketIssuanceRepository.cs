using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.Aggregate;
using DomainDrivenDesignExample.API.SharedKernels.Repositories;

namespace DomainDrivenDesignExample.API.BoundedContexts.Ticketing.TicketingAggregate;

public interface ITicketIssuanceRepository : IGenericRepository<TicketIssuance>
{
    Task<List<TicketIssuance>> GetConfirmedTicketsIssuanceByScheduleIdAndScreeningDate(Guid scheduleId,
        DateOnly ScreeningDate);
}