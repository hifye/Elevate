using MediatR;

namespace Application.Features.Auth.Commands.Token;

public record TokenCommand(
    string Token,
    string RefreshToken,
    DateTime TokenExpiration,
    DateTime RefreshTokenExpiration) : IRequest;