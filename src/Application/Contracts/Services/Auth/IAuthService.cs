using Application.DTOs.Auth;

namespace Application.Contracts.Services.Auth;

public interface IAuthService
{
    Task<TokenResponse> Login(LoginRequest request);
    Task Register(RegisterRequest request);
}