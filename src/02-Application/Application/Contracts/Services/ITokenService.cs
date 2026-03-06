using System.Security.Claims;
using Application.Shared.DTOs;
using Domain.Entities.Auth;

namespace Application.Contracts.Services;

public interface ITokenService
{
    Task<TokenResponse> Token(User user);
    string RefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    DateTime TokenExpiration();
    DateTime RefreshTokenExpiration();
}