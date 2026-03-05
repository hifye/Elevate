namespace Application.DTOs.Auth;

public record TokenResponse(
    string Token,
    string RefreshToken,
    DateTime TokenExpiration,
    DateTime RefreshTokenExpiration
);
