namespace DomainDrivenDesignExample.API.BoundedContexts.Catalog.SupplierCustomerContextMap;

public record GetCatalogInfoResponse(string CinemaName, string HallName, string MovieTitle, short SeatCount);