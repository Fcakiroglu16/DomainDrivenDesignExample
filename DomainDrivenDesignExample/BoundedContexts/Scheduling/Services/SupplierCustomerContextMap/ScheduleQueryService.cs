using DomainDrivenDesignExample.API.SharedKernels;
using System.Net;

namespace DomainDrivenDesignExample.API.BoundedContexts.Scheduling.Services.SupplierCustomerContextMap;

public class ScheduleQueryService(IScheduleRepository scheduleRepository, ILogger<ScheduleQueryService> logger)
    : IScheduleQueryService
{
    public async Task<AppResult<GetScheduleInfoResponse>> GetScheduleInfo(Guid scheduleId)
    {
        Schedule? schedule = await scheduleRepository.GetByIdAsync(scheduleId);
        if (schedule is null)
        {
            logger.LogWarning("Schedule with Id {scheduleId} was not found", scheduleId);
            //return appDependencyService.LocalizeError.Error<GetScheduleInfoResponse>(ErrorCodes.ScheduleNotFound,
            //    HttpStatusCode.NotFound);
        }

        return AppResult<GetScheduleInfoResponse>.SuccessAsOk(new GetScheduleInfoResponse(schedule!.HallId,
            schedule.MovieId, schedule.ShowTime, schedule.TicketPrice));
    }
}