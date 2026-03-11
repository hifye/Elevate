namespace Application.Interfaces.Services;

public interface ICurrentUserService
{
    public Guid UserId { get; }
    string Email { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
}