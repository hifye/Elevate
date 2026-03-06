using System.Security.Claims;
using Application.Security;
using Domain.Entities.Auth;

namespace Application.Interfaces.Services;

public interface ITokenService
{
    TokenResult Token(User user);
    string RefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    DateTime TokenExpiration();
    DateTime RefreshTokenExpiration();
}