namespace Application.Shared.DTOs;

public record TokenResponse(
    string Token,
    string RefreshToken,
    DateTime TokenExpiration,
    DateTime RefreshTokenExpiration
);
