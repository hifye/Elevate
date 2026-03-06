using System.Security.Claims;
using Application.Contracts.Services;
using Application.Shared.DTOs;
using Domain.Entities.Auth;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    public Task<TokenResponse> Token(User user)
    {
        throw new NotImplementedException();
    }

    public string RefreshToken()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        throw new NotImplementedException();
    }

    public DateTime TokenExpiration()
    {
        throw new NotImplementedException();
    }

    public DateTime RefreshTokenExpiration()
    {
        throw new NotImplementedException();
    }
}