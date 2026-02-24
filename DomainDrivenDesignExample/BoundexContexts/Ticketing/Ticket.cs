using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.ValueObjects;

namespace DomainDrivenDesignExample.API.BoundexContexts.Ticketing
{
    public class Ticket : BaseEntity<Guid>
    {
        public Price Price { get; set; } = null!;


        //own type
        // Id, UserId,  Money, Currency
    }
}
