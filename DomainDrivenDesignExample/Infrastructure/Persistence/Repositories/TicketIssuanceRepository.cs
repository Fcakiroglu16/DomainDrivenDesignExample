#region

using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.Aggregate;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.TicketingAggregate;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;

internal class TicketIssuanceRepository(AppDbContext context)
    : GenericRepository<TicketIssuance>(context), ITicketIssuanceRepository
{
    public async Task<List<TicketIssuance>> GetConfirmedTicketsIssuanceByScheduleIdAndScreeningDate(
        Guid scheduledMovieShowId, DateOnly screeningDate)
    {
        return await Context.TicketIssuances
            .Where(x => x.ScreeningDate == screeningDate && x.ScheduledMovieShowId == scheduledMovieShowId &&
                        x.Status == TicketIssuanceStatus.Confirmed)
            .ToListAsync();
    }

    public async Task<TicketIssuance?> Get(Guid CustomerId, DateOnly ScreeningDate, Guid ScheduledMovieShowId)
    {
        TicketIssuance? localTicketIssuance = Context.TicketIssuances.Local.FirstOrDefault(x =>
            x.CustomerId == CustomerId && x.ScreeningDate == ScreeningDate &&
            x.ScheduledMovieShowId == ScheduledMovieShowId);

        if (localTicketIssuance is not null) return localTicketIssuance;

        return await Context.TicketIssuances.FirstOrDefaultAsync(x =>
            x.CustomerId == CustomerId && x.ScreeningDate == ScreeningDate &&
            x.ScheduledMovieShowId == ScheduledMovieShowId);
    }


    public Task<TicketIssuance> Get(Guid customerId, Guid TicketIssuanceId)
    {
        return Context.TicketIssuances.SingleAsync(x => x.CustomerId == customerId && x.Id == TicketIssuanceId);
    }

    public List<TicketIssuance> GetTicketsPurchaseByScheduleIdAndScreeningDate(Guid scheduleId, DateOnly ScreeningDate)
    {
        return Context.TicketIssuances
            .Where(x => x.ScheduledMovieShowId == scheduleId && x.ScreeningDate == ScreeningDate)
            .ToList();
    }

    public Task Confirm(Guid customerId, Guid TicketIssuanceId)
    {
        throw new NotImplementedException();
    }
}