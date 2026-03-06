using System.Security.Claims;
using Application.Contracts.Services;
using Application.Shared.DTOs;
using Domain.Entities.Auth;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    
    public Task<TokenResponse> Token(User user)
    {
        //TODO: Implementar a lógica de geração do Token
        throw new NotImplementedException();
    }

    public string RefreshToken()
    {
        //TODO: Implementar a lógica de geração do Refresh Token
        throw new NotImplementedException();
    }
    
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        //TODO: Implementar a lógica de obtenção de informações do Token JWT expirado
        throw new NotImplementedException();
    }

    public DateTime TokenExpiration()
    {
        //TODO: Implementar a lógica de expiração do Token JWT
        throw new NotImplementedException();
    }

    public DateTime RefreshTokenExpiration()
    {
        //TODO: Implementar a lógica de expiração do Refresh Token JWT
        throw new NotImplementedException();
    }
}