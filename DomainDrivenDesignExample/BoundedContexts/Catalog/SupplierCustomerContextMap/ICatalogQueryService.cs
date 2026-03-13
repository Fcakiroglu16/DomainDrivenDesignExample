using DomainDrivenDesignExample.API.SharedKernels;

namespace DomainDrivenDesignExample.API.BoundedContexts.Catalog.SupplierCustomerContextMap;

public interface ICatalogQueryService
{
    Task<AppResult<GetCatalogInfoResponse>> GetCinemaInfo(Guid hallId, Guid movieId);
}