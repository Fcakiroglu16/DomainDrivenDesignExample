using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.TicketingAggregate;
using DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesignExample.API.Infrastructure.Identities;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserContext, UserContext>();


        return services;
    }
}