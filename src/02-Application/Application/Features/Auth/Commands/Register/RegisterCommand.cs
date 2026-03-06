using MediatR;

namespace Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    string Name,
    string Email,
    string Password,
    string ConfirmPassword ) : IRequest;