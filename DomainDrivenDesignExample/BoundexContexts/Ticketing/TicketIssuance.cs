using CinemaTicketingSystem.SharedKernel.AggregateRoot;
using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.ValueObjects;

namespace DomainDrivenDesignExample.API.BoundexContexts.Ticketing
{
    public class TicketIssuance : AggregateRoot<Guid>
    {
        public decimal TotalPrice { get; set; }

        public Guid CustomerId { get; set; }


        //Navigation property
        public List<Ticket> Tickets { get; set; }
    }
}