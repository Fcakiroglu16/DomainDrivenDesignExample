using DomainDrivenDesignExample.API.SharedKernels;

namespace DomainDrivenDesignExample.API.Infrastructure.Identities
{
    public class UserContext : IUserContext
    {
        public Guid UserId => Guid.CreateVersion7();
        public string UserName => "ahmet16";
        public string Email => "ahmet16@outlook.com";
    }
}
