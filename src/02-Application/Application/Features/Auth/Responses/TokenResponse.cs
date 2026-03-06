using Domain.Commom;
using MediatR;

namespace Application.Features.Auth.Responses;

public record TokenResponse(
    string Token,
    string RefreshToken,
    DateTime TokenExpiration,
    DateTime RefreshTokenExpiration
);
