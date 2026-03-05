namespace Application.DTOs.Auth;

public record RegisterRequest(
    string Name,
    string Email,
    string PasswordHash,
    string ConfirmPassword
);
