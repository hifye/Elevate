using System.Security.Claims;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Identity;

public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
{
    private readonly ClaimsPrincipal _user = accessor.HttpContext?.User!;
    
    public bool IsAuthenticated => _user.Identity?.IsAuthenticated ?? false;

    public Guid UserId => Guid.Parse(accessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public string Email => _user.FindFirstValue(ClaimTypes.Email)!;
    
    public IEnumerable<string> Roles => _user.FindAll(ClaimTypes.Role).Select(x => x.Value);
}