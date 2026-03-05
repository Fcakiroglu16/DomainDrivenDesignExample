using DomainDrivenDesignExample.API.SharedKernels;

namespace DomainDrivenDesignExample.API.BoundedContexts.Scheduling.Services.SupplierCustomerContextMap;

public class ScheduleQueryService : IScheduleQueryService
{
    public Task<AppResult<GetScheduleInfoResponse>> GetScheduleInfo(Guid ScheduledMovieShowId)
    {
        throw new NotImplementedException();
    }
}