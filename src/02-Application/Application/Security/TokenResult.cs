namespace Application.Security;

public record TokenResult(
    string Token,
    string RefreshToken,
    DateTime TokenExpiration,
    DateTime RefreshTokenExpiration);