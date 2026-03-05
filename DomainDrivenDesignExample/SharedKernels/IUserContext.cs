namespace DomainDrivenDesignExample.API.SharedKernels;

public interface IUserContext
{
    Guid UserId { get; }
    string UserName { get; }
    string Email { get; }
}